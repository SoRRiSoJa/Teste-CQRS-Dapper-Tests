using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories;

public interface IContaCorrenteRepository
{
    Task<ContaCorrente> GetContaCorrenteById(string contaCorrenteId);
    Task<ContaCorrente> GetContaCorrenteByNumero(long numeroContaCorrente);
}
