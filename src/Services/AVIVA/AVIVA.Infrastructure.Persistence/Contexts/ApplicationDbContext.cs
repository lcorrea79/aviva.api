using System.Data;
using AVIVA.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AVIVA.Domain.Entities;
using AVIVA.Infrastructure.Persistence.Configurations;

namespace AVIVA.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// Database context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Current tenant id
        /// </summary>
        public string CurrentTenantId { get; set; }

        /// <summary>
        /// Current tenant connection string
        /// </summary>
        public string CurrentTenantConnectionString { get; set; }

        /// <summary>
        /// Connection for Dapper
        /// </summary>
        public IDbConnection Connection => Database.GetDbConnection();

        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            _dateTime = dateTime;
            _loggerFactory = loggerFactory;


        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Fee> Fees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

        }
    }
}