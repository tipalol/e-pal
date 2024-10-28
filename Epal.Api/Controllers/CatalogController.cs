using Epal.Api.Controllers.Base;
using Epal.Application.Features.Catalog.ServiceTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class CatalogController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public async Task GetServiceTypes()
        => await Sender.Send(new ServiceTypesCatalogRequest());
}
