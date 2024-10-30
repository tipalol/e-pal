using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Admin.ServiceTypes.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Admin;


public class CategoriesController(ISender sender) : RestController(sender)
{
    // TODO Get All, Add, Remove
    
    [HttpPost]
    public async Task<Result> AddOrUpdate(AddOrUpdateCategoryRequest request)
        => await Sender.Send(request);
}
