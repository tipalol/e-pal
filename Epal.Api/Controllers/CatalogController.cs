using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Catalog.ServiceTypes;
using Epal.Application.Features.Catalog.ServiceTypes.Get;
using Epal.Application.Features.Catalog.ServiceTypes.Models;
using Epal.Application.Features.Services.AddOrUpdate.Models;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class CatalogController(ISender sender) : RestController(sender)
{
    [HttpGet("/GetByTypes")]
    public async Task<IEnumerable<ServiceType>> GetServiceTypes(int TakeCount)
        => await Sender.Send(new ServiceTypesCatalogRequest(TakeCount));

}

class MyClass
{
    
}