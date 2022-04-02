using DotNetNinja.Dojo.Constants;
using DotNetNinja.Dojo.Models;

namespace DotNetNinja.Dojo.Entities;

public class DojoLocation
{
    public string EntityName { get; set; } = string.Empty;
    public string EntityKind { get; set; } = string.Empty;

    public LocationScheme Scheme { get; set; } = LocationScheme.Dojo;
    public Uri Identifier { get; set; } = Location.None;

    public Location ToModel()
    {
        return new Location
        {
            Identifier = Identifier,
            Scheme = Scheme
        };
    }
}