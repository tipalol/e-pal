using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class RestController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}