using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omf.CustomerManagementService.DomainModel;

namespace Omf.CustomerManagementService.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(ConfigureUser);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(m => m.UserId)
                .IsRequired();
            builder.Property(m => m.UserEmail)
                .HasMaxLength(70)
                .IsRequired();
            builder.Property(m => m.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(m => m.LastName)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
