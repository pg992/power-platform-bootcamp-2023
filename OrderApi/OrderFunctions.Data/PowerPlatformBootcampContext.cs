using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrderFunctions.Data.Models;

namespace OrderFunctions.Data
{
    public partial class PowerPlatformBootcampContext : DbContext
    {
        public PowerPlatformBootcampContext()
        {
        }

        public PowerPlatformBootcampContext(DbContextOptions<PowerPlatformBootcampContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Order { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOnUtc).HasColumnType("datetime");

                entity.Property(e => e.ProductName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
