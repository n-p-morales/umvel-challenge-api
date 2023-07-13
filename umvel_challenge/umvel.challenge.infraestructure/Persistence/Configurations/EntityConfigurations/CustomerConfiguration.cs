using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Customers;
using umvel.challenge.infraestructure.Persistence.Contexts;

namespace umvel.challenge.infraestructure.Persistence.Configurations.EntityConfigurations
{
    public class CustomerConfiguration
    {
        public static void Configure (ModelBuilder builder)
        {
            builder
                .Entity<Customer>(entity => {
                    entity.ToTable("Customer");
                });

            builder
                .Entity<Customer>()
                .HasKey(nameof(Customer.CustomerId))
                .HasName($"pk_customerId");

            builder
                .Entity<Customer>()
                .Property(e => e.Name)
                .HasColumnType(SqlDataTypes.Varchar)
                .HasColumnName("Name");

            builder
                .Entity<Customer>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Customer_Sales_CustomerId");
        }
    }
}
