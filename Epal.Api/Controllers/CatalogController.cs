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
    
    [HttpGet("epals")]
    public async Task<IEnumerable<ProfileView>> GetEpalProfiles(Guid? serviceTypeId, int take = 20, SortingType sort = SortingType.None)
        => await Sender.Send(new EpalsByServiceTypeCatalogRequest(serviceTypeId, sort, take));
}
