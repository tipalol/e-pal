using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Catalog.ServiceTypes.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Admin;

[Authorize]
public class ServiceTypesController(ISender sender) : RestController(sender)
{
    // TODO Get All, Add, Remove
    
    [HttpPost]
    public async Task<Result> AddOrUpdate(ServiceTypesDto serviceTypesDto)
        => await Sender.Send(serviceTypesDto);
}
