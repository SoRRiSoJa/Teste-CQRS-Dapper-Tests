using FluentAssertions;
using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Repositories;

namespace BancoTests;

public class SaldoContaCorrenteQueryHandlerTests
{
    [Fact]
    public async Task Handle_WithValidQuery_ReturnsSaldoContaResponse()
    {
        // Arrange
        var numeroContaCorrente = 123;
        var contaCorrenteId = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var movimentacaoRepository = Substitute.For<IMovimentoRepository>();
        decimal spectedValue = 100;
        var handler = new SaldoContaCorrenteQueryHandler(contaCorrenteRepository, movimentacaoRepository);

        var query = new GetContaCorrenteByNumeroQuery(numeroContaCorrente);


        var contaCorrente = new ContaCorrente
        {
            IdContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
            Nome = "Katherine Sanchez",
            Numero = query.Numero,
            Ativo = true
        };
        var saldoContaResponse = new SaldoContaResponse(contaCorrente.Nome, query.Numero, spectedValue);

        var movimentosCredito = new List<Movimento>
        {
            new Movimento(contaCorrenteId, TipoMovimentacao.C, 100),
            new Movimento(contaCorrenteId, TipoMovimentacao.C, 50),
        };

        var movimentosDebito = new List<Movimento>
        {
            new Movimento(contaCorrenteId, TipoMovimentacao.D, 30),
            new Movimento(contaCorrenteId, TipoMovimentacao.D, 20),
        };

        contaCorrenteRepository.GetContaCorrenteByNumero(query.Numero).Returns(Task.FromResult(contaCorrente));
        movimentacaoRepository.GetAllByIdContaCorrenteAndTipoMovimento(Arg.Any<string>(), Arg.Any<TipoMovimentacao>())
            .Returns(Task.FromResult((IEnumerable<Movimento>)movimentosCredito), Task.FromResult((IEnumerable<Movimento>)movimentosDebito));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        saldoContaResponse.DataRepososta = result.DataRepososta;
        // Assert

        await contaCorrenteRepository.Received(1).GetContaCorrenteByNumero(Arg.Is<long>(numeroContaCorrente));
        await movimentacaoRepository.Received(1).GetAllByIdContaCorrenteAndTipoMovimento(Arg.Is<string>(contaCorrente.IdContaCorrente), Arg.Is<TipoMovimentacao>(TipoMovimentacao.C));
        saldoContaResponse.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Handle_WithInvalidAccount_ThrowsInvalidAccountException()
    {
        // Arrange
        var numeroContaCorrente = -1234;
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var movimentacaoRepository = Substitute.For<IMovimentoRepository>();

        var handler = new SaldoContaCorrenteQueryHandler(contaCorrenteRepository, movimentacaoRepository);

        var query = new GetContaCorrenteByNumeroQuery(numeroContaCorrente);

        contaCorrenteRepository.GetContaCorrenteByNumero(query.Numero).Returns(Task.FromResult<ContaCorrente>(null));

        // Act & Assert

        await Assert.ThrowsAsync<InvalidAccountException>(() => handler.Handle(query, CancellationToken.None));
    }
}
