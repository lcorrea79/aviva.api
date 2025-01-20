#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Interfaces;

/// <summary>
/// Database connection for read operations
/// </summary>
public interface IApplicationReadDbConnection
{
    /// <summary>
    /// Query database and return result
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Query database and return result
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="map"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="splitOn"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(string sql, Func<T1, T2, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default);

    /// <summary>
    /// Map query result to object
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="map"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="splitOn"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(string sql, Func<T1, T2, T3, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default);

    /// <summary>
    /// Query first or default
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Query a single row from database and return result
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
}