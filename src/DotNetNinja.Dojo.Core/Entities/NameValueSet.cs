namespace DotNetNinja.Dojo.Entities;

public abstract class NameValueSet
{
    protected NameValueSet():this(string.Empty, string.Empty)
    {
    }

    protected NameValueSet(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public string Value { get; set; }
}