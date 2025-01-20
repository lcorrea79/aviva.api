using AVIVA.Application.Interfaces;
using AVIVA.Infrastructure.Persistence.Contexts;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AVIVA.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Get all entities with pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Get all entities with pagination and order by
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPagedAdvancedResponseAsync(int pageNumber, int pageSize, string orderBy, string fields)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select<T>("new(" + fields + ")")
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        /// <summary>
        /// Bulk insert entities
        /// </summary>
        /// <param name="entities"></param>
        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            // Bulk Insert Extension https://entityframework-extensions.net/bulk-insert
            await _dbContext.BulkInsertAsync(entities);

            // if DB does not support bulk insert use the code below
            //foreach (T row in entities)
            //{
            //    await this.AddAsync(row);
            //}
        }
    }
}