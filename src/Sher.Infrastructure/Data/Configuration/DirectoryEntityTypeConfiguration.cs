using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;

namespace Sher.Infrastructure.Data.Configuration
{
    public class DirectoryEntityTypeConfiguration : IEntityTypeConfiguration<Directory>
    {
        public void Configure(EntityTypeBuilder<Directory> builder)
        {
            builder.HasOne<Directory>().WithMany().HasForeignKey(d => d.ParentDirectoryId);
            builder.HasOne<Uploader>().WithMany().HasForeignKey(d => d.UploaderId);
            builder.HasIndex(d => d.Name);
        }
    }
}