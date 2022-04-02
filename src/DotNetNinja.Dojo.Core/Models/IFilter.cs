namespace DotNetNinja.Dojo.Models;

public interface IFilter<T> where T: class
{
    IQueryable<T> Filter(IQueryable<T> data);
}