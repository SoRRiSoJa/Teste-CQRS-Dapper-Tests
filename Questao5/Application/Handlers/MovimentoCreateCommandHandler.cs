using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Repositories;
using System.Text.Json;

namespace Questao5.Application.Handlers;

public class MovimentoCreateCommandHandler : IRequestHandler<CreateMovimentacaoRequestCommand, Movimento>
{
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    public MovimentoCreateCommandHandler(IMovimentoRepository movimentoRepository, IContaCorrenteRepository contaCorrenteRepository, IIdempotenciaRepository idempotenciaRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _movimentoRepository = movimentoRepository;
        _idempotenciaRepository = idempotenciaRepository;

    }
    public async Task<Movimento> Handle(CreateMovimentacaoRequestCommand request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);

        var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByNumero(request.NumeroContaCorrente);

        ValidateContaCorrente(contaCorrente);

        var movimento = new Movimento(contaCorrente.IdContaCorrente, request.TipoMovimentacao, request.Valor);
        var idempotencia = BuildIdempotencia(request, movimento);
        var result = await _idempotenciaRepository.GetIdempotenciaByChave(idempotencia.Chave_Idempotencia);

        if (result is null)
        {
            await _idempotenciaRepository.CreateIndepotencia(idempotencia);
            movimento = await _movimentoRepository.CreateMovimento(movimento);
        }
        else
        {
            movimento = JsonSerializer.Deserialize<Movimento>(idempotencia.Resultado);
        }

        return movimento;
    }

    private Idempotencia BuildIdempotencia(CreateMovimentacaoRequestCommand request, Movimento? movimento)
    {
        var jsonRequest = JsonSerializer.Serialize(request);
        var jsonResultado = JsonSerializer.Serialize(movimento);
        return new Idempotencia(request.RequisicaoId, jsonRequest, jsonResultado);
    }

    private static void ValidateRequest(CreateMovimentacaoRequestCommand request)
    {

        if (string.IsNullOrWhiteSpace(request.RequisicaoId))
        {
            throw new InvalidRequesIdException();
        }
        if (request.Valor < 0)
        {
            throw new InvalidValueException();
        }

        if (request.TipoMovimentacao is not TipoMovimentacao.C and not TipoMovimentacao.D)
        {
            throw new InvalidTypeException();
        }
    }
    private void ValidateContaCorrente(ContaCorrente? contaCorrente)
    {
        if (contaCorrente is null)
        {
            throw new InvalidAccountException();
        }
        if (!contaCorrente.Ativo)
        {
            throw new InactiveAccountException();
        }
    }
}
