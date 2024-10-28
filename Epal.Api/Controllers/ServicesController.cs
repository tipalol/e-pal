using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Services.Get;
using Epal.Application.Features.Services.GetAll;
using Epal.Application.Features.Services.Models;
using Epal.Application.Features.Services.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ServicesController(ISender sender) : RestController(sender)
{
    // TODO а в этом смысла нет, тк нет сценария использования
    [HttpGet("{id:guid}")]
    public async Task<Result<ActivityModel>> GetServiceById([FromRoute(Name = "id")]Guid id)
        => await Sender.Send(new ActivityRequest(id));
    // TODO а тут нужен фильтр по типам услуг
    [HttpGet]
    public async Task<Result<IEnumerable<ActivityModel>>> GetServices()
        => await Sender.Send(new GetAllActiviesRequest());
    // TODO услуга добавляется из под пользователя, а еще каждая услуга привязана к типу услуг
    [HttpPost]
    public async Task<Result> AddService(CreateActivityRequest request)
        => await Sender.Send(request);

}
