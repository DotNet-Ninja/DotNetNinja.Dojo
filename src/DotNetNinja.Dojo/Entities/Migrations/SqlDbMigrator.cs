using DotNetNinja.Dojo.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Dojo.Entities.Migrations;

public class SqlDbMigrator: IDbMigrator
{
    private readonly DojoContext _db;
    private readonly DbSettings _settings;
    private DbConfiguration DojoConfiguration => _settings.Contexts[nameof(DojoContext)];

    public SqlDbMigrator(DojoContext db, DbSettings settings)
    {
        _db = db;
        _settings = settings;
    }

    public IDbMigrator Migrate()
    {
        if (DojoConfiguration.EnableAutomaticMigrations)
        {
            _db.Database.Migrate();
        }
        return this;
    }

    public IDbMigrator SeedDatabase()
    {
        if (DojoConfiguration.EnableDataSeeding)
        {
            if (!_db.Entities.Any())
            {
                var entities = new List<DojoEntity>
                {
                    new DojoEntity()
                    {
                        Kind = "Component",
                        Name = "Dojo-UI",
                        Type = "WebApp",
                        Location = new(),
                        Labels = new List<Label>
                        {
                            new Label{Name = "Type", Value = "Application Catalog"}
                        },
                        Annotations = new List<Annotation>(),
                        Description = "Software Catalog",
                        Tags = new List<Tag>
                        {
                            new Tag{Name = "Blazor"}
                        }
                    },
                    new DojoEntity()
                    {
                        Kind = "Component",
                        Name = "Dojo-Api",
                        Type = "WebService",
                        Location = new(),
                        Labels = new List<Label>
                        {
                            new Label{Name = "Type", Value = "Application Catalog API"}
                        },
                        Annotations = new List<Annotation>(),
                        Description = "Software Catalog API",
                        Tags = new List<Tag>
                        {
                            new Tag{Name = "API"},
                            new Tag{Name = "C#"},
                            new Tag{Name = "OpenApi"}
                        }
                    }
                };
                _db.Entities.AddRange(entities);
                _db.SaveChanges();
            }
        }

        return this;
    }
}