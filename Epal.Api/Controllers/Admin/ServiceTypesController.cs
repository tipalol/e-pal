using Epal.Api.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Epal.Api.Controllers.Admin;

[Authorize]
public class ServiceTypesController(ISender sender) : RestController(sender)
{
    // TODO Get All, Add, Remove
}
