using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVIVA.Application.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get all entities with pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Get all entities with pagination and order by
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPagedAdvancedResponseAsync(int pageNumber, int pageSize, string orderBy, string fields);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Delete entity by id
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<T> entities);
    }
}