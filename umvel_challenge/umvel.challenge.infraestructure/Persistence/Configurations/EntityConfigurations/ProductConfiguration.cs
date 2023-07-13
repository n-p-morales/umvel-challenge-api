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
    public class ProductConfiguration
    {
        public static void Configure (ModelBuilder builder)
        {
            builder.Entity<Product>(entity => {
                entity.ToTable("Product");
            });

            builder
                .Entity<Product>()
                .HasKey(nameof(Product.ProductId))
                .HasName($"pk_products");

            builder
                .Entity<Product>()
                .Property(e => e.ProductId)
                .HasColumnType(SqlDataTypes.Int)
                .HasColumnName("ProductId");

            builder
                .Entity<Product>()
                .Property(e => e.Name)
                .HasColumnType(SqlDataTypes.Varchar)
                .HasColumnName("Name");

            builder
                .Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasColumnType(SqlDataTypes.Varchar)
                .HasColumnName("UnitPrice");

            builder
                .Entity<Product>()
                .Property(e => e.Cost)
                .HasColumnType(SqlDataTypes.Decimal)
                .HasColumnName("Cost");

            builder
                .Entity<Product>()
                .HasOne(e => e.Concept)
                .WithOne(e => e.Product)
                .HasForeignKey("umvel.challenge.domain.entities.Concept", "ProductId");
        }
    }
}
