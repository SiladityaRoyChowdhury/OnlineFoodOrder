using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Omf.OrderManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>(ConfigureOrder);
            builder.Entity<Menu>(ConfigureMenu);
        }

        private void ConfigureMenu(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.Property(m => m.MenuId)
                .IsRequired();
            
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(c => c.OrderId)
                .IsRequired();
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(o => o.OrderId);
        }
    }
}
