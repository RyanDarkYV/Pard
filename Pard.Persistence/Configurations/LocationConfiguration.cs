using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pard.Domain.Entities.Locations;

namespace Pard.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();

            builder.HasOne(x => x.Record)
                .WithOne(x => x.Location)
                .HasForeignKey<Location>(x => x.RecordId);
        }
    }
}