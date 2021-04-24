using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Access.Users;
using Sher.Core.Files.Uploaders;

namespace Sher.Infrastructure.Data.Configuration
{
    public class UploaderEntityTypeConfiguration : IEntityTypeConfiguration<Uploader>
    {
        public void Configure(EntityTypeBuilder<Uploader> builder)
        {
            builder.HasOne<User>().WithMany().HasForeignKey(u => u.UserId);
        }
    }
}