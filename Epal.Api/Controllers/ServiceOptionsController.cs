using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.ServiceOptions.AddOrUpdate;
using Epal.Application.Features.ServiceOptions.AddOrUpdate.Models;
using Epal.Application.Features.ServiceOptions.Get;
using Epal.Application.Features.ServiceOptions.Get.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ServiceOptionsController(ISender sender) : RestController(sender)
{
    /// <summary>
    /// Получение списка опций для услуги в профиле
    /// </summary>
    [HttpGet("{profileId:guid}/service/{serviceId:guid}")]
    public async Task<Result<IEnumerable<ServiceOptionListView>>> Get([FromRoute(Name = "profileId")] Guid profileId, [FromRoute(Name = "serviceId")] Guid serviceId)
        => await Sender.Send(new GetServiceOptionsRequest(profileId, serviceId));
    
    /// <summary>
    /// Добавление или обновление опции для услуги
    /// </summary>
    [HttpPost, Authorize]
    public async Task<Result> AddOrUpdate(ServiceOptionDto serviceOptionDto)
        => await Sender.Send(new CreateOrUpdateServiceOptionRequest(serviceOptionDto));
}