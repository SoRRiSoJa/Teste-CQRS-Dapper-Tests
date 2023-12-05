﻿using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public IdempotenciaRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;

    }
    public async Task<Idempotencia> CreateIndepotencia(Idempotencia idempotencia)
    {

        await using var connection = new SqliteConnection(_databaseConfig.Name);
        var sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@Chave_Idempotencia, @Requisicao, @Resultado)";
        await connection.ExecuteAsync(sql, idempotencia);
        return idempotencia;
    }

    public async Task<Idempotencia> GetIdempotenciaByChave(string chaveIdempotencia)
    {
        await using var connection = new SqliteConnection(_databaseConfig.Name);
        var sql = "SELECT * FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
        return await connection.QuerySingleOrDefaultAsync<Idempotencia>(sql, new { ChaveIdempotencia = chaveIdempotencia });
    }
}

