using DotNetNinja.Dojo.Annotations;

namespace DotNetNinja.Dojo.Models;

public class MetaData
{
    [EntityName]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Dictionary<string,string>? Annotations { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
    public List<string>? Tags { get; set; }
    public Location Location { get; set; } = new();
}