using DotNetNinja.Dojo.Models;

namespace DotNetNinja.Dojo.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> data, IFilter<T> filter) where T : class
    {
        return filter.Filter(data);
    }

    public static Page<T> ToPage<T>(this IQueryable<T> data, IPagingFilter<T> filter) where T : class
    {
        return filter.Apply(data);
    }

    public static Task<Page<T>> ToPageAsync<T>(this IQueryable<T> data, IPagingFilter<T> filter) where T : class
    {
        return filter.ApplyAsync(data);
    }
}