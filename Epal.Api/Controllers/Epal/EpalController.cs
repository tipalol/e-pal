using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Admin.Users.GetAll;
using Epal.Application.Features.Epals.GetAll;
using Epal.Application.Features.Profiles.Get;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Features.Profiles.Post;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Epal;

public class EpalController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public async Task<Result<IEnumerable<EpalProfile>>> GetUsers()
        => await Sender.Send(new GetEpalsRequest());
}