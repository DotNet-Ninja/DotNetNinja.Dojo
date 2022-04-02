using DotNetNinja.Dojo.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotNetNinja.Dojo.Entities.TypeConfigurations;

public class DojoLocationTypeConfiguration : IEntityTypeConfiguration<DojoLocation>
{
    public void Configure(EntityTypeBuilder<DojoLocation> builder)
    {
        builder.ToTable("Locations");
        builder.HasKey(e => new
        {
            e.EntityKind,
            e.EntityName
        }).IsClustered().HasName("PK_Locations");
        builder.Property(e => e.EntityKind).IsRequired().HasMaxLength(64);
        builder.Property(e => e.EntityName).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Scheme)
            .HasConversion<EnumToStringConverter<LocationScheme>>()
            .HasMaxLength(32)
            .IsRequired();
        builder.Property(e => e.Identifier).IsRequired().HasMaxLength(2048);
    }
}