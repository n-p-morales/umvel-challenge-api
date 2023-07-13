using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using umvel.challenge.domain;
using umvel.challenge.domain.Commons.EntitiesBase;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Concepts;
using umvel.challenge.domain.Entities.Customers;
using umvel.challenge.infraestructure.Persistence.Configurations.EntityConfigurations;

namespace umvel.challenge.infraestructure.Persistence.Contexts
{
    public class UmvelChallengeDbContext : DbContext, IUmvelChallengeDbContext
    {
        public UmvelChallengeDbContext(DbContextOptions<UmvelChallengeDbContext> options) : base(options) { }

        public DbSet<Concept> Concepts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public void BeginTransaction()
        {
            Database.BeginTransaction();
        }

        public void BeginTransactionAsync(CancellationToken cancellationToken)
        {
            Database.BeginTransactionAsync(cancellationToken);
        }

        public void CommitTransaction()
        {
            Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }

        public void TrackEntityChanges()
        {
            var newEntities = ChangeTracker
                .Entries<IAuditableEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedEntities = ChangeTracker
                .Entries<IAuditableEntity>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            var now = DateTime.UtcNow;

            foreach (var added in newEntities)
            {
                added.Created = now;
                added.LastModified = now;
            }

            foreach (var modified in modifiedEntities)
            {
                modified.LastModified = now;
            }
        }

        public override int SaveChanges()
        {
            TrackEntityChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackEntityChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConceptConfiguration.Configure(modelBuilder);
            CustomerConfiguration.Configure(modelBuilder);
            ProductConfiguration.Configure(modelBuilder);
            SaleConfiguration.Configure(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
