using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Services.Add;
using Epal.Application.Features.Services.Add.Models;
using Epal.Application.Features.Services.Get;
using Epal.Application.Features.Services.Get.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ServicesController(ISender sender) : RestController(sender)
{
    [HttpGet("{profileId:guid}/category/{categoryId:guid}")]
    public async Task<Result<IEnumerable<ServiceListView>>> Get([FromRoute(Name = "profileId")] Guid profileId, [FromRoute(Name = "categoryId")] Guid categoryId)
        => await Sender.Send(new GetServicesRequest(profileId, categoryId));

    [HttpPost, Authorize]
    public async Task<Result> AddService(ServiceDto serviceDto)
        => await Sender.Send(new CreateServiceRequest(serviceDto));
}
