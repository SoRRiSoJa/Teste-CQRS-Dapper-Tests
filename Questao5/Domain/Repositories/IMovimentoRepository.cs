using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Repositories;

public interface IMovimentoRepository
{
    Task<Movimento> CreateMovimento(Movimento movimento);
    Task<IEnumerable<Movimento>> GetAllByIdContaCorrenteAndTipoMovimento(string idContaCorrente, TipoMovimentacao tipoMovimentacao);

}
