using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Catalog.Categories.Get;
using Epal.Application.Features.Catalog.Categories.Get.Models;
using Epal.Application.Features.Catalog.Profiles.Get;
using Epal.Application.Features.Catalog.Profiles.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class CatalogController(ISender sender) : RestController(sender)
{
    [HttpGet("categories")]
    public async Task<PaginatedResult<CategoryCatalogView>> GetServiceTypes(int take = 6, int skip = 0)
        => await Sender.Send(new ServiceTypesCatalogRequest(take, skip));
    
    [HttpGet("epals")]
    public async Task<PaginatedResult<ProfileView>> GetEpalProfiles(Guid? serviceTypeId, int take = 20, int skip = 0, SortingType sort = SortingType.None)
        => await Sender.Send(new EpalsByServiceTypeCatalogRequest(serviceTypeId, sort, take, skip));
}
