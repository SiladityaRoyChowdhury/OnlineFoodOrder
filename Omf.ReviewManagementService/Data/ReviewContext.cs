using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omf.ReviewManagementService.DomainModel;
using System;

namespace Omf.ReviewManagementService.Data
{
    public class ReviewContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public ReviewContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(ConfigureReview);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.Property(c => c.ReviewId)
                .IsRequired();
            builder.Property(c => c.RestaurantId)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(c => c.Comments)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(c => c.Rating)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(c => c.UserId)
                .IsRequired();
        }
    }
}