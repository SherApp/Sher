using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Files;
using Sher.Core.Identity;

namespace Sher.Infrastructure.Data.Configuration
{
    public class UploaderEntityTypeConfiguration : IEntityTypeConfiguration<Uploader>
    {
        public void Configure(EntityTypeBuilder<Uploader> builder)
        {
            builder.HasKey(u => u.Id.Value);
            builder.HasOne<User>().WithOne().HasForeignKey<User>(u => u.Id);
            builder.ToTable("Users");
        }
    }
}