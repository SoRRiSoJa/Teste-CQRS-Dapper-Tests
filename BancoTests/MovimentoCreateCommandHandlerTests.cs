using FluentAssertions;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Repositories;

namespace BancoTests;


public class MovimentoCreateCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidRequest_CreatesAndReturnsMovimento()
    {
        // Arrange
        var numeroContaCorrente = 123;
        
        var valor = new decimal(50.00);
        var movimentoRepository = Substitute.For<IMovimentoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        
        var handler = new MovimentoCreateCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository);

        var request = new CreateMovimentacaoRequestCommand(Guid.NewGuid().ToString(), numeroContaCorrente, valor, TipoMovimentacao.C);

        var contaCorrente = new ContaCorrente
        {
            IdContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
            Numero=request.NumeroContaCorrente,
            Nome= "Katherine Sanchez",
            Ativo = true
        };
        
        contaCorrenteRepository.GetContaCorrenteByNumero(request.NumeroContaCorrente).Returns(Task.FromResult(contaCorrente));

        var expectedMovimento = new Movimento(contaCorrente.IdContaCorrente, request.TipoMovimentacao, request.Valor);
        movimentoRepository.CreateMovimento(Arg.Any<Movimento>()).Returns(Task.FromResult(expectedMovimento));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        expectedMovimento.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ThrowsException()
    {
        // Arrange
        var requisicaoId = string.Empty;
        var numumeroContaCorrente = 0;
        var tipoMovimentacao = TipoMovimentacao.C;
        var valor = new decimal(-50.00);
        
        var movimentoRepository = Substitute.For<IMovimentoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();

        var handler = new MovimentoCreateCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository);

        var request = new CreateMovimentacaoRequestCommand(requisicaoId,numumeroContaCorrente,valor,tipoMovimentacao);
        

        contaCorrenteRepository.GetContaCorrenteByNumero(request.NumeroContaCorrente).Returns(Task.FromResult<ContaCorrente>(null));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRequesIdException>(() => handler.Handle(request, CancellationToken.None));
    }
}
