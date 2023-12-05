using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IContaCorrenteRepository, ContaCorrenteRepository>();
        services.AddTransient<IMovimentoRepository, MovimentoRepository>();
        services.AddTransient<IIdempotenciaRepository, IdempotenciaRepository>();
    }
    public static void AddMediatRApi(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateMovimentacaoRequestCommand));
        services.AddMediatR(typeof(GetContaCorrenteByNumeroQuery));
    }
}
