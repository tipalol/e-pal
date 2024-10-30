using Epal.Api.Controllers.Base;
using Epal.Application.Common;
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
    public async Task<PaginatedResult<ServiceTypeCatalogView>> GetServiceTypes(int take = 6, int skip = 0)
        => await Sender.Send(new ServiceTypesCatalogRequest(take, skip));
    
    [HttpGet("epals")]
    public async Task<PaginatedResult<ProfileView>> GetEpalProfiles(Guid? serviceTypeId, int take = 20, int skip = 0, SortingType sort = SortingType.None)
        => await Sender.Send(new EpalsByServiceTypeCatalogRequest(serviceTypeId, sort, take, skip));
}
