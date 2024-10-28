using Epal.Api.Controllers.Base;
using Epal.Application.Features.Catalog.ServiceTypes;
using MediatR;

namespace Epal.Api.Controllers;

public class CatalogController(ISender sender) : RestController(sender)
{
    public async Task GetServiceTypes()
        => await Sender.Send(new ServiceTypesCatalogRequest());
}
