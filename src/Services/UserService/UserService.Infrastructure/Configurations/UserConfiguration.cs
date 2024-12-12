using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.Address)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
