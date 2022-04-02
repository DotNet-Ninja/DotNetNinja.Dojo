using AutoMapper;

using DotNetNinja.Dojo.Entities;
using DotNetNinja.Dojo.Extensions;
using DotNetNinja.Dojo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Dojo.Controllers;

public class EntitiesController:ApiDataController
{
    private IQueryable<DojoEntity> Projection => 
                                    Db.Entities.Include(e => e.Annotations)
                                                .Include(e => e.Labels)
                                                .Include(e => e.Location);
    private IOrderedQueryable<DojoEntity> SortedProjection => Projection.OrderBy(e => e.Name);

    public EntitiesController(ILogger<EntitiesController> logger, IMapper mapper, DojoContext context) : base(logger, mapper, context)
    {
    }

    /// <summary>
    /// Retrieves an entity by kind &amp; name.
    /// </summary>
    /// <param name="kind">The kind of the entity</param>
    /// <param name="name">The name of the entity</param>
    /// <response code="404">Entity not found.</response>
    /// <response code="200">Entity returned.</response>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     GET /api/v1/Entities/Component/Dojo-UI
    /// </remarks>
    [HttpGet("{kind}/{name}")]
    [ProducesDefaultResponseType(typeof(Entity))]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(typeof(Entity), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEntity([FromRoute] string kind, [FromRoute] string name)
    {
        ValidateRequiredRouteParameter(kind, nameof(kind));
        ValidateRequiredRouteParameter(name, nameof(name));
        if (!ModelState.IsValid)
        {
            return InvalidModelState();
        }

        var entity = await Projection.SingleOrDefaultAsync(e => e.Kind == kind && e.Name == name);
        if (entity == null)
        {
            return NotFound();
        }

        var model = Mapper.Map<Entity>(entity);

        return Ok(model);
    }

    /// <summary>
    /// Returns a paged set of Entities ordered by name.
    /// </summary>
    /// <param name="filter">Specify paging parameters.</param>
    /// <response code="404">Entity not found.</response>
    /// <response code="200">Entity returned.</response>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     GET /api/v1/Entities?page=1&amp;size=25
    /// </remarks>
    [HttpGet]
    [ProducesDefaultResponseType(typeof(Entity))]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(typeof(Entity), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEntities([FromQuery]EntityFilter filter)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entities = await SortedProjection.ApplyFilter(filter).ToPageAsync(filter);
        if (!entities.Items.Any())
        {
            return NotFound();
        }

        var model = Mapper.Map<Page<Entity>>(entities);
        
        return Ok(model);
    }
}