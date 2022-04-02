using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetNinja.Dojo.Entities.TypeConfigurations;

public class LabelTypeConfiguration:IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        builder.HasKey(e => new { e.Name, e.Value }).IsClustered().HasName("PK_Labels");
        builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Value).IsRequired().HasMaxLength(256);
    }
}