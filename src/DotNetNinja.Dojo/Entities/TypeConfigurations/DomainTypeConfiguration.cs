using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetNinja.Dojo.Entities.TypeConfigurations;

public class DomainTypeConfiguration: IEntityTypeConfiguration<Domain>
{
    public void Configure(EntityTypeBuilder<Domain> builder)
    {
        builder.HasKey(e => e.Id).IsClustered().HasName("PK_Domains");
        builder.Property(e => e.Id).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
    }
}