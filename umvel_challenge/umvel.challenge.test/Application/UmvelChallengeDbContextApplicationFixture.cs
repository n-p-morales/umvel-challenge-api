using System;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Customers.Rules;
using umvel.challenge.domain.Entities.Products.Rules;
using umvel.challenge.domain.Entities.Sales.Rule;
using umvel.challenge.infraestructure.Persistence.Contexts;

namespace umvel.challenge.test.Application
{
	public class UmvelChallengeDbContextApplicationFixture : IDisposable
	{
		public readonly UmvelChallengeDbContext context;

		public readonly CustomerAggregate customerAggregate;

		public readonly ProductAggregate productAggregate;

		public readonly SaleAggregate saleAggregate;

        public UmvelChallengeDbContextApplicationFixture()
		{
			var options = new DbContextOptionsBuilder<UmvelChallengeDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			context = new UmvelChallengeDbContext(options);

			context.Customers.Add(Customer.Create(2, "Jose de Jesus Morales Martinez"));
            context.Customers.Add(Customer.Create(3, "Felipe Villegas Preciado"));
            context.Customers.Add(Customer.Create(4, "Pedro Ivan Salas Peña"));

			context.Products.Add(Product.Create(2, "Martillo", "200.00", 200.0));
            context.Products.Add(Product.Create(3, "Destornillador", "150.00", 150.0));
            context.Products.Add(Product.Create(4, "Clavo", "2.00", 2.0));

			context.Sales.Add(Sale.Create(2, Convert.ToDateTime("13/07/2023"), 1, 400.00));
            context.Sales.Add(Sale.Create(3, Convert.ToDateTime("13/07/2023"), 1, 300.00));
            context.Sales.Add(Sale.Create(4, Convert.ToDateTime("13/07/2023"), 1, 40.00));

			context.SaveChanges();

			customerAggregate = new CustomerAggregate(
				new CustomerIdMustExistRule(context));

			productAggregate = new ProductAggregate(
				new ProductIdMustExistRule(context));

			saleAggregate = new SaleAggregate(
				new SaleIdMustExistRule(context));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            context.Dispose();
        }

    }
}

