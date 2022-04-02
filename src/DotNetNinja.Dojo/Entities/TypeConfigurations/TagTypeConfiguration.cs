using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetNinja.Dojo.Entities.TypeConfigurations;

public class TagTypeConfiguration: IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(e => e.Name).IsClustered().HasName("PK_Tags");
        builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

        builder.HasMany(e => e.Entities).WithMany(e => e.Tags);
    }
}