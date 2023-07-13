using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities;
using umvel.challenge.infraestructure.Persistence.Contexts;

namespace umvel.challenge.infraestructure.Persistence.Configurations.EntityConfigurations
{
    public class SaleConfiguration
    {
        public static void Configure (ModelBuilder builder)
        {
            builder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");
            });

            builder
                .Entity<Sale>()
                .HasKey(nameof(Sale.SaleId))
                .HasName($"pk_sales");

            builder
                .Entity<Sale>()
                .Property(e => e.SaleId)
                .HasColumnType(SqlDataTypes.Int)
                .HasColumnName("SalesId");

            builder
                .Entity<Sale>()
                .Property(e => e.Date)
                .HasColumnType(SqlDataTypes.Date)
                .HasColumnName("Date");

            builder
                .Entity<Sale>()
                .Property(e => e.Total)
                .HasColumnType(SqlDataTypes.Decimal)
                .HasColumnName("Total");

            builder
                .Entity<Sale>()
                .HasOne(e => e.Customer)
                .WithMany(e => e.Sales)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Sales_Customer_CustomerId");
        }
    }
}
