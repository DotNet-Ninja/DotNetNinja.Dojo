using AutoMapper;

using DotNetNinja.Dojo.Constants;
using DotNetNinja.Dojo.Entities;

using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.Dojo.Controllers;

[Produces(MimeType.ApplicationJson, MimeType.ApplicationXYaml, MimeType.ApplicationXml)]
[Consumes(MimeType.ApplicationJson, MimeType.ApplicationXYaml, MimeType.ApplicationXml)]
public class ApiDataController: ApiController
{
    protected DojoContext Db { get; }

    public ApiDataController(ILogger logger, IMapper mapper, DojoContext context) : base(logger, mapper)
    {
        Db = context;
    }
}