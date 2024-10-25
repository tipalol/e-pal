using Epal.Api.Controllers.Base;
using Epal.Application.Features.Registration.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Auth;

public class RegisterController(ISender sender) : RestController(sender)
{
    [HttpPost]
    public async Task AddUser(CreateRegisterDtoRequest request)
        => await Sender.Send(request);
}