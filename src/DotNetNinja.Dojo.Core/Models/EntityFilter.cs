using DotNetNinja.Dojo.Entities;

namespace DotNetNinja.Dojo.Models;

public class EntityFilter: PagingFilter<DojoEntity>, IFilter<DojoEntity>
{
    public IQueryable<DojoEntity> Filter(IQueryable<DojoEntity> data)
    {
        return data;
    }
}