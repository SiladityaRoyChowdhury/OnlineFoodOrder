using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omf.RestaurantManagementService.DomainModel;
using System;

namespace Omf.RestaurantManagementService.Data
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }

        public RestaurantContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(ConfigureRestaurant);
            modelBuilder.Entity<Menu>(ConfigureMenu);
            modelBuilder.Entity<RestaurantMenu>(ConfigureRestaurantMenu);
        }

        private void ConfigureRestaurantMenu(EntityTypeBuilder<RestaurantMenu> builder)
        {
            builder.ToTable("RestaurantMenu");
            builder.HasKey(rm => new { rm.RestaurantId, rm.MenuId });
            builder.HasOne<Restaurant>(rm => rm.Restaurant)
                .WithMany(r => r.RestaurantMenus)
                .HasForeignKey(rm => rm.RestaurantId);
            builder.HasOne<Menu>(m => m.Menu)
                .WithMany(rm => rm.RestaurantMenus)
                .HasForeignKey(m => m.MenuId);
            builder.Property(c => c.RestaurantId)
                .IsRequired();
            builder.Property(c => c.MenuId)
                .IsRequired();
        }

        private void ConfigureRestaurant(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant");
            builder.Property(c => c.RestaurantId)
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(c => c.Rating)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ListedCity)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ApproxCost)
                .IsRequired();
        }

        private void ConfigureMenu(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.Property(c => c.MenuId)
                .IsRequired();
            builder.Property(c => c.Item)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Price)
                .IsRequired();
            builder.Property(c => c.Quantity)
                .IsRequired();
        }
    }
}
