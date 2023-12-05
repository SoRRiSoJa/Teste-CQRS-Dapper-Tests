using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using Questao5.Infrastructure.Services.Controllers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Commands.Requests;
using MediatR;
using Questao5.Domain.Enumerators;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Questao5.Tests;
using FluentAssert;

namespace BancoTests;

public class ContaCorrenteControllerTests
{
    [Fact]
    public async Task SaldoContaCorrente_ReturnsOkResult()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        var controller = new ContaCorrenteController(mediator);
        var numeroContaCorrente = 123456;
        var titular = "Joao da Silva";
        var saldoContaCorrente = new decimal(100.00);
        var saldo = new SaldoContaResponse(titular, numeroContaCorrente, saldoContaCorrente);
        mediator.Send(Arg.Any<GetContaCorrenteByNumeroQuery>())
            .Returns(Task.FromResult(saldo));

        // Act
        var result = await controller.SaldoContaCorrente(numeroContaCorrente);
        var okObjectResult = result as OkObjectResult;

        // Assert
        saldo.Should().BeEquivalentTo(okObjectResult.Value);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var getContaCorrenteByNumeroQuery = new GetContaCorrenteByNumeroQuery(numeroContaCorrente);
        await mediator.Received(1).Send(Arg.Is<GetContaCorrenteByNumeroQuery>(x => x.CompareObjects(getContaCorrenteByNumeroQuery)));
    }

    [Fact]
    public async Task Movimentar_ReturnsOkResult()
    {
        // Arrange
        var requisicaoId = "CHAVE9999";
        var contaCorrenteId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";
        var numeroContaCorrente = 123;
        var valor = new Decimal(100.00);
        var mediator = Substitute.For<IMediator>();
        var tipoMovimentacao = TipoMovimentacao.C;
        var movimento = new Movimento(contaCorrenteId, tipoMovimentacao, valor);
        var controller = new ContaCorrenteController(mediator);
        var movimentacaoRequest = new CreateMovimentacaoRequestCommand(requisicaoId, numeroContaCorrente, valor, tipoMovimentacao);

        mediator.Send(Arg.Any<CreateMovimentacaoRequestCommand>())
            .Returns(Task.FromResult(movimento));

        // Act
        var result = await controller.Movimentar(movimentacaoRequest);
        var okObjectResult = result as OkObjectResult;
        // Assert
        movimento.Should().BeEquivalentTo(okObjectResult.Value);
        await mediator.Received(1).Send(Arg.Is<CreateMovimentacaoRequestCommand>(x => x.CompareObjects(movimentacaoRequest)));

    }
}
