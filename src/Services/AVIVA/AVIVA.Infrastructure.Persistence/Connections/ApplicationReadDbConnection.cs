#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using AVIVA.Application.Interfaces;
using Microsoft.Data.SqlClient;

namespace AVIVA.Infrastructure.Persistence.Connections;

/// <summary>
/// Database connection for read operations
/// </summary>
public class ApplicationReadDbConnection : IApplicationReadDbConnection, IDisposable
{
    private readonly IDbConnection _connection;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="currentTenantService"></param>
    public ApplicationReadDbConnection()
    {
        _connection = new SqlConnection("");
    }

    /// <summary>
    /// Query database and return result
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return (await _connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(string sql, Func<T1, T2, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
    {
        return await _connection.QueryAsync(sql, map, param, transaction, true, splitOn);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(string sql, Func<T1, T2, T3, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
    {
        return await _connection.QueryAsync(sql, map, param, transaction, true, splitOn);
    }

    /// <inheritdoc />
    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    /// <inheritdoc />
    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QuerySingleAsync<T>(sql, param, transaction);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _connection.Dispose();
    }
}