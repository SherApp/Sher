using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Files;
using Sher.Core.Files.Uploaders;

namespace Sher.Infrastructure.Data.Configuration
{
    public class FileEntityTypeConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasOne<Uploader>().WithMany().HasForeignKey(u => u.UploaderId);
            builder.HasIndex(f => f.FileName);
        }
    }
}