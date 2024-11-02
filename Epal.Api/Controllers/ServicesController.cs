using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Services.AddOrUpdate;
using Epal.Application.Features.Services.AddOrUpdate.Models;
using Epal.Application.Features.Services.Get;
using Epal.Application.Features.Services.Get.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ServicesController(ISender sender) : RestController(sender)
{
    /// <summary>
    /// Получение списка услуг, которые оказывает епал
    /// </summary>
    [HttpGet("{profileId:guid}")]
    public async Task<Result<IEnumerable<ServiceListView>>> GetTypes([FromRoute(Name = "profileId")] Guid profileId)
        => await Sender.Send(new GetServicesRequest(profileId));
    
    /// <summary>
    /// Добавление или обновление услуги
    /// </summary>
    [HttpPost, Authorize]
    public async Task<Result> AddOrUpdate(ServiceDto serviceDto)
        => await Sender.Send(new CreateOrUpdateServiceRequest(serviceDto));
}
