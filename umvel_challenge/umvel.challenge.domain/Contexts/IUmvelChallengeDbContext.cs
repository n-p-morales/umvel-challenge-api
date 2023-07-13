using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Concepts;
using umvel.challenge.domain.Entities.Customers;

namespace umvel.challenge.domain.Contexts
{
    public interface IUmvelChallengeDbContext : IDisposable
    {
        DbSet<Concept> Concepts { get; set; }

        DbSet<Customer> Customers { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<Sale> Sales { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void BeginTransaction();

        void BeginTransactionAsync(CancellationToken cancellationToken);

        void CommitTransaction();

        void RollbackTransaction();

        int SaveChanges();

        void TrackEntityChanges();
    }
}
