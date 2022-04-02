using DotNetNinja.Dojo.Entities;

namespace DotNetNinja.Dojo.Extensions;

public static class EntityExtensions
{
    public static IList<TPair> ToSet<TPair>(this IDictionary<string, string> source) 
            where TPair: NameValueSet, new()
    {
        return source.Keys.Select(key => new TPair
                {
                    Name = key, Value = source[key]
                }).ToList();
    }

    public static void Update<TPair>(this Dictionary<string, string> source, IList<TPair> target)
        where TPair : NameValueSet, new()
    {
        source.ToSet<TPair>().Update(target);
    }

    public static void Update<TPair>(this IList<TPair> source, IList<TPair> target)
        where TPair : NameValueSet, new()
    {
        var targetLength = target.Count();
        var sourceNames = source.Select(s => s.Name).Distinct();
        for (int index = targetLength - 1; index >= 0; index--)
        {
            var current = target[index];
        }
    }
}