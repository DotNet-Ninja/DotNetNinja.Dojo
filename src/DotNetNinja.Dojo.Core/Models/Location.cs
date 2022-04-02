using DotNetNinja.Dojo.Constants;
using DotNetNinja.Dojo.Entities;
using YamlDotNet.Serialization;

namespace DotNetNinja.Dojo.Models;

public class Location
{
    public static readonly Uri None = new("dojo://location/none");

    public LocationScheme Scheme { get; set; } = LocationScheme.Dojo;

    [YamlMember(typeof(string))]
    public Uri Identifier { get; set; } = None;

    public void FromEntity(DojoLocation location)
    {
        Identifier = location.Identifier;
        Scheme = location.Scheme;
    }

    public DojoLocation ToEntity()
    {
        var location = new DojoLocation();
        UpdateEntity(location);
        return location;
    }

    public void UpdateEntity(DojoLocation location)
    {
        location.Identifier = Identifier;
        location.Scheme = Scheme;
    }
}