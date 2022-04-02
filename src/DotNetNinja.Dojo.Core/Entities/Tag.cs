namespace DotNetNinja.Dojo.Entities;

public class Tag
{
    public string Name { get; set; } = string.Empty;
    public List<DojoEntity>? Entities { get; set; }
}