using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetNinja.Dojo.Entities.TypeConfigurations;

public class DojoEntityTypeConfigurations: IEntityTypeConfiguration<DojoEntity>
{
    public void Configure(EntityTypeBuilder<DojoEntity> builder)
    {
        builder.ToTable("Entities");
        builder.HasKey(e=> new {e.Kind, e.Name}).IsClustered(); //.HasName("PK_Entities");
        builder.Property(e => e.Kind).IsRequired().HasMaxLength(64);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Description).HasMaxLength(256);
        builder.HasMany(e => e.Annotations).WithMany(e=>e.Entities).UsingEntity("EntityAnnotations");
        builder.HasMany(e => e.Labels).WithMany(e => e.Entities).UsingEntity("EntityLabels");
        builder.HasMany(e => e.Tags).WithMany(e => e.Entities).UsingEntity("EntityTags");
        builder.HasOne(e => e.Location).WithOne().HasForeignKey(typeof(DojoLocation), "EntityKind", "EntityName");
        builder.Property(e => e.Type).HasMaxLength(128);
    }
}