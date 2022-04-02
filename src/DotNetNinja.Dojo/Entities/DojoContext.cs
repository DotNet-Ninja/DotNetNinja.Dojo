using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Dojo.Entities;

public class DojoContext:DbContext
{
    // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #pragma warning disable CS8618 
    public DojoContext(DbContextOptions<DojoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StartUp).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DojoEntity> Entities { get; set; }
    public DbSet<DojoLocation> Locations { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Domain> Domains { get; set; }
    public DbSet<Annotation> Annotations { get; set; }
    public DbSet<Label> Labels { get; set; }

}