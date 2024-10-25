using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class RestController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}