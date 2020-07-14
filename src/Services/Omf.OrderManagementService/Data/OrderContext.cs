using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omf.OrderManagementService.DomainModel;

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
            builder.HasKey(m => new { m.OrderId, m.MenuId });
            //    builder.HasOne<Order>(m => m.Order)
            //        .WithMany(o => o.OrderItems)
            //        .HasForeignKey(m => m.OrderId);         
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(c => c.OrderId)
                .IsRequired();
            builder.HasMany<Menu>(o => o.OrderItems)
                .WithOne(m=>m.Order)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
