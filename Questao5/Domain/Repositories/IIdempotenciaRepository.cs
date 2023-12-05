using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories;

public interface IIdempotenciaRepository
{
    Task<Idempotencia> GetIdempotenciaByChave(string chaveIdempotencia);
    Task<Idempotencia> CreateIndepotencia(Idempotencia idempotencia);
}
