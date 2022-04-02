namespace DotNetNinja.Dojo.Entities.Migrations;

public interface IDbMigrator
{
    IDbMigrator Migrate();
    IDbMigrator SeedDatabase();
}