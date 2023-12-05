using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;

public class GetContaCorrenteByNumeroQuery : IRequest<SaldoContaResponse>
{
    public GetContaCorrenteByNumeroQuery(int numeroContaCorrente)
    {
            Numero=numeroContaCorrente;
    }
    public  int Numero { get; set; }
}
