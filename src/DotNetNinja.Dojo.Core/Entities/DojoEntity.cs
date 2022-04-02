using System.Globalization;

namespace DotNetNinja.Dojo.Entities;

public class DojoEntity
{
    public string Id => $"{Kind.ToLowerInvariant()}|{Name.ToLowerInvariant()}";
    public string Kind { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Annotation> Annotations { get; set; } = new();
    public List<Label> Labels { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
    public DojoLocation Location { get; set; } = new();
    public string? Type { get; set; }
}