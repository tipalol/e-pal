using Epal.Api.Controllers.Base;
using Epal.Application.Features.Admin.Users.Get;
using Epal.Application.Features.Catalog.Profiles.Get;
using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Features.Catalog.ServiceTypes.Get;
using Epal.Application.Features.Catalog.ServiceTypes.Get.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class CatalogController(ISender sender) : RestController(sender)
{
    [HttpGet("serviceTypes")]
    public async Task<IEnumerable<ServiceTypeCatalogView>> GetServiceTypes(int take)
        => await Sender.Send(new ServiceTypesCatalogRequest(take));
    [HttpGet("{id:guid}")]
    public async Task<IEnumerable<ProfileView>> GetNewEpalProfiles([FromRoute(Name = "id")] Guid id)
        => await Sender.Send(new NewProfilesRequest(5));
}
