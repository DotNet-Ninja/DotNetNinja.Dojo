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

    protected ApiController(ILogger logger)
    {
        Logger = logger;
    }
}