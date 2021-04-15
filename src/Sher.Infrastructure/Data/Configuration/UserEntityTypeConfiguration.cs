using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Access.Users;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne<Uploader>().WithOne().HasForeignKey<Uploader>(u => u.Id);
            builder.ToTable("Users");
            builder.HasIndex(u => u.EmailAddress).IsUnique();
            builder.Navigation(u => u.Roles).HasField("_roles");
            builder.Navigation(u => u.Clients).HasField("_clients");
            builder.OwnsMany(u => u.Roles);
            builder.OwnsOne(u => u.Password);
        }
    }
}