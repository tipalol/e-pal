using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Services.AddOrUpdate;
using Epal.Application.Features.Services.AddOrUpdate.Models;
using Epal.Application.Features.Services.ExtraInfo.AddOrUpdate;
using Epal.Application.Features.Services.ExtraInfo.AddOrUpdate.Models;
using Epal.Application.Features.Services.ExtraInfo.Get;
using Epal.Application.Features.Services.ExtraInfo.Get.Model;
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
    /// Получение дополнительной информации о конкретной услуге
    /// </summary>
    [HttpGet("ExtraInfo/{serviceId:guid}")]
    public async Task<Result<ServiceExtraInfoView>> GetServiceExtraInfo([FromRoute(Name = "serviceId")] Guid serviceId)
        => await Sender.Send(new GetServicesExtraInfoRequest(serviceId));
    
    /// <summary>
    /// Обновление дополнительной информации о конкретной услуге
    /// </summary>
    [HttpPost("ExtraInfo/{serviceId:guid}"), Authorize]
    public async Task<Result> AddOrUpdate(ServiceExtraInfoDto serviceExtraInfoDto)
        => await Sender.Send(new AddOrUpdateServicesExtraInfoRequest(serviceExtraInfoDto));


    
    /// <summary>
    /// Добавление или обновление услуги
    /// </summary>
    [HttpPost, Authorize]
    public async Task<Result> AddOrUpdate(ServiceDto serviceDto)
        => await Sender.Send(new CreateOrUpdateServiceRequest(serviceDto));
}
