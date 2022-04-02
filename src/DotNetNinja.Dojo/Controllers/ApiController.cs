using System.Net;

using AutoMapper;

using DotNetNinja.Dojo.Constants;

using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.Dojo.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MimeType.ApplicationJson, MimeType.ApplicationXYaml, MimeType.ApplicationXml)]
[Consumes(MimeType.ApplicationJson, MimeType.ApplicationXYaml, MimeType.ApplicationXml)]
public abstract class ApiController: ControllerBase
{
    protected ILogger Logger { get; }
    protected IMapper Mapper { get; }

    protected ApiController(ILogger logger, IMapper mapper)
    {
        Logger = logger;
        Mapper = mapper;
    }

    protected void ValidateRequiredRouteParameter(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ModelState.AddModelError(name, $"Required parameter {name} cannot be null or empty.");
        }
    }

    protected BadRequestObjectResult InvalidModelState()
    {
        return BadRequest(ModelState);
    }

    protected StatusCodeResult StatusCode(HttpStatusCode status)
    {
        return StatusCode((int)status);
    }

    protected StatusCodeResult NotImplemented()
    {
        return StatusCode(HttpStatusCode.NotImplemented);
    }
}