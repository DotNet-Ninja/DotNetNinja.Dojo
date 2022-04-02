using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Dojo.Models;

public abstract class PagingFilter<T>: IPagingFilter<T> where T: class
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 25;

    public Page<T> Apply(IQueryable<T> data)
    {
        var items = data.Skip((Page - 1) * Size).Take(Size).ToList();
        var count = data.Count();
        return new Page<T>
        {
            Items = items,
            Number = Page,
            Size = Size,
            TotalItemCount = count
        };
    }

    public async Task<Page<T>> ApplyAsync(IQueryable<T> data)
    {
        var items = await data.Skip((Page - 1) * Size).Take(Size).ToListAsync();
        var count = await data.CountAsync();
        return new Page<T>
        {
            Items = items,
            Number = Page,
            Size = Size,
            TotalItemCount = count
        };
    }
}