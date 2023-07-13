using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Concepts;
using umvel.challenge.infraestructure.Persistence.Contexts;

namespace umvel.challenge.infraestructure.Persistence.Configurations.EntityConfigurations
{
    public class ConceptConfiguration
    {
        public static void Configure (ModelBuilder builder)
        {
            builder.Entity<Concept>(entity =>
            {
                entity.ToTable("Concept");
            });

            builder
                .Entity<Concept>()
                .HasKey(nameof(Concept.ConceptId))
                .HasName($"pk_concept");

            builder
                .Entity<Concept>()
                .Property(e => e.ConceptId)
                .HasColumnType(SqlDataTypes.Int)
                .HasColumnName("ConceptId");

            builder
                .Entity<Concept>()
                .Property(e => e.Quantity)
                .HasColumnType(SqlDataTypes.Decimal)
                .HasColumnName("Quantity");

            builder
                .Entity<Concept>()
                .Property(e => e.ProductId)
                .HasColumnType(SqlDataTypes.Int)
                .HasColumnName("ProductId");

            builder
                .Entity<Concept>()
                .Property(e => e.SaleId)
                .HasColumnType(SqlDataTypes.Int)
                .HasColumnName("SaleId");

            builder
                .Entity<Concept>()
                .Property(e => e.UnitPrice)
                .HasColumnType(SqlDataTypes.Decimal)
                .HasColumnName("UnitPrice");

            builder
                .Entity<Concept>()
                .Property(e => e.Amount)
                .HasColumnType(SqlDataTypes.Decimal)
                .HasColumnName("Amount");

            builder
                .Entity<Concept>()
                .HasOne(e => e.Sale)
                .WithMany(e => e.Concepts)
                .HasForeignKey(e => e.SaleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Sale_Concept_SaleId");

            builder
                .Entity<Concept>()
                .HasOne(e => e.Product)
                .WithOne(e => e.Concept)
                .HasForeignKey("umvel.challenge.domain.entities.Products", "ProductId");

        }
    }
}
