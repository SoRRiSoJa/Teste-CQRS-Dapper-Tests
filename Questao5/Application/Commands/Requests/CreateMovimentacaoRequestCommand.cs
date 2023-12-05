using MediatR;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests;

public class CreateMovimentacaoRequestCommand :  IRequest<Movimento>
{
    public CreateMovimentacaoRequestCommand(string requisicaoId,int numeroContaCorrente, decimal valor, TipoMovimentacao tipoMovimentacao)
    {
        RequisicaoId = requisicaoId;
        NumeroContaCorrente = numeroContaCorrente;
        TipoMovimentacao= tipoMovimentacao;
        Valor = valor;
    }
    public string RequisicaoId { get; set; }
    public int NumeroContaCorrente { get; set; }
    public decimal Valor { get; set; }
    public TipoMovimentacao TipoMovimentacao { get; set; }
    public override int GetHashCode()
    {
        return HashCode.Combine(RequisicaoId, NumeroContaCorrente, Valor, TipoMovimentacao);
    }
}
