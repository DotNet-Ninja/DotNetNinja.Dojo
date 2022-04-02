namespace DotNetNinja.Dojo.Models;

public interface IPagingFilter<T> where T: class
{
    int Page { get; set; }
    int Size { get; set; }

    Page<T> Apply(IQueryable<T> data);

    Task<Page<T>> ApplyAsync(IQueryable<T> data);
}