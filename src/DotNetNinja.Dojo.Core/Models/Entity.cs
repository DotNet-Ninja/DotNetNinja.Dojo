using DotNetNinja.Dojo.Annotations;

namespace DotNetNinja.Dojo.Models;

public class Entity
{
    public string ApiVersion => Constants.ApiVersion.V1;
    public string Id => $"{Kind.ToLowerInvariant()}|{MetaData.Name.ToLowerInvariant()}";

    [EntityName]
    public string Kind { get; set; } = string.Empty;
    public MetaData MetaData { get; set; } = new();

}

public abstract class Entity<TSpec> : Entity where TSpec : Specification, new()
{
    public TSpec Spec { get; set; } = new();
}