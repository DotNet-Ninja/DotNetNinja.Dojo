using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetNinja.Dojo.Entities.TypeConfigurations;

public class AnnotationTypeConfiguration: IEntityTypeConfiguration<Annotation>
{
    public void Configure(EntityTypeBuilder<Annotation> builder)
    {
        builder.HasKey(e=> new{e.Name, e.Value}).IsClustered().HasName("PK_Annotations");
        builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Value).IsRequired().HasMaxLength(256);
    }
}