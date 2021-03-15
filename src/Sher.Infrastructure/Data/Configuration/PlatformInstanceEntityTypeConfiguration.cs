using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sher.Core.Access;

namespace Sher.Infrastructure.Data.Configuration
{
    public class PlatformInstanceEntityTypeConfiguration : IEntityTypeConfiguration<PlatformInstance>
    {
        public void Configure(EntityTypeBuilder<PlatformInstance> builder)
        {
            builder.HasKey("_id");
            builder.OwnsOne(p => p.Settings);
        }
    }
}