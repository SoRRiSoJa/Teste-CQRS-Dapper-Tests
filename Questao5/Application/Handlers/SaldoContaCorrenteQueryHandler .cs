using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Handlers;

public class SaldoContaCorrenteQueryHandler : IRequestHandler<GetContaCorrenteByNumeroQuery, SaldoContaResponse>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMovimentoRepository _movimentacaoRepository;
    public SaldoContaCorrenteQueryHandler(IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentacaoRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<SaldoContaResponse> Handle(GetContaCorrenteByNumeroQuery request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);
        var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByNumero(request.Numero) ?? throw new InvalidAccountException();

        if (!contaCorrente.Ativo)
        {
            throw new InactiveAccountException();
        }

        var movimentoCreditoTask = _movimentacaoRepository.GetAllByIdContaCorrenteAndTipoMovimento(contaCorrente.IdContaCorrente, TipoMovimentacao.C);
        var movimentoDebitoTask = _movimentacaoRepository.GetAllByIdContaCorrenteAndTipoMovimento(contaCorrente.IdContaCorrente, TipoMovimentacao.D);

        await Task.WhenAll(movimentoCreditoTask, movimentoDebitoTask);

        var movimentoCredito = movimentoCreditoTask.Result;
        var movimentoDebito = movimentoDebitoTask.Result;

        var saldoContaCorrente = movimentoCredito.Sum(c => c.Valor) - movimentoDebito.Sum(d => d.Valor);

        return new SaldoContaResponse(contaCorrente.Nome, contaCorrente.Numero, saldoContaCorrente);
    }
    private void ValidateRequest(GetContaCorrenteByNumeroQuery request)
    {
        var strNumeroConta = request.Numero.ToString();
        if (request.Numero <= 0)
        {
            throw new InvalidAccountException();
        }
        if (strNumeroConta.Length < 3 || strNumeroConta.Length > 10)
        {
            throw new InvalidAccountException();
        }
    }
}
