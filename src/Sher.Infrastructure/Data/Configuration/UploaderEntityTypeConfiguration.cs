using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data.Configuration
{
    public class UploaderEntityTypeConfiguration : IEntityTypeConfiguration<Uploader>
    {
        public void Configure(EntityTypeBuilder<Uploader> builder)
        {
            builder.ToTable("Users");
        }
    }
}