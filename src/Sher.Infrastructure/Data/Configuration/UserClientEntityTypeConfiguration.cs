using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Access.Users;

namespace Sher.Infrastructure.Data.Configuration
{
    public class UserClientEntityTypeConfiguration : IEntityTypeConfiguration<UserClient>
    {
        public void Configure(EntityTypeBuilder<UserClient> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedNever();
        }
    }
}