using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Services.AddOrUpdate;
using Epal.Application.Features.Services.AddOrUpdate.Models;
using Epal.Application.Features.Services.ById;
using Epal.Application.Features.Services.Get;
using Epal.Application.Features.Services.Get.Models;
using Epal.Application.Features.Services.GetCategories;
using Epal.Application.Features.Services.GetCategories.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ServicesController(ISender sender) : RestController(sender)
{
    [HttpGet("{profileId:guid}/categories")]
    public async Task<Result<IEnumerable<CategoryListViewWithPrice>>> GetTypes([FromRoute(Name = "profileId")] Guid profileId)
        => await Sender.Send(new GetCategoriesRequest(profileId));

    [HttpGet("{profileId:guid}/category/{categoryId:guid}")]
    public async Task<Result<IEnumerable<ServiceListView>>> Get([FromRoute(Name = "profileId")] Guid profileId, [FromRoute(Name = "categoryId")] Guid categoryId)
        => await Sender.Send(new GetServicesRequest(profileId, categoryId));

    [HttpGet("category/{categoryId:guid}")]
    public async Task<Result<CategoryListView>> Get([FromRoute(Name = "categoryId")] Guid categoryId)
        => await Sender.Send(new GetCategoryById(categoryId));

    [HttpPost, Authorize]
    public async Task<Result> AddOrUpdate(ServiceDto serviceDto)
        => await Sender.Send(new CreateOrUpdateServiceRequest(serviceDto));
}
