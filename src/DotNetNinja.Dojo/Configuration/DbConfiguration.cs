namespace DotNetNinja.Dojo.Configuration;

public class DbConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public DbType Type { get; set; } = DbType.SqlServer;
    public bool EnableAutomaticMigrations { get; set; }
    public bool EnableDataSeeding { get; set; }
}