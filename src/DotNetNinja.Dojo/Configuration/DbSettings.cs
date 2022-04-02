using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.Dojo.Configuration
{
    [AutoBind("DbSettings")]
    public class DbSettings
    {
        public Dictionary<string, DbConfiguration> Contexts { get; set; } = new();
    }
}