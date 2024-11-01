using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Categories.ById;
using Epal.Application.Features.Categories.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class CategoriesController(ISender sender) : RestController(sender)
{
    /// <summary>
    /// Получение категории по айди
    /// </summary>
    [HttpGet("{categoryId:guid}")]
    public async Task<Result<CategoryListView>> Get([FromRoute(Name = "categoryId")] Guid categoryId)
        => await Sender.Send(new GetCategoryById(categoryId));
}