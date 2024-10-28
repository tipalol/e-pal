using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Activity.Get;
using Epal.Application.Features.Activity.Get.MyActivies;
using Epal.Application.Features.Activity.GetAll;
using Epal.Application.Features.Activity.Models;
using Epal.Application.Features.Activity.Post;
using Epal.Application.Features.Profiles.Get;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Features.Profiles.Post;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ActivityController(ISender sender) : RestController(sender)
{
    [HttpGet("{id:guid}")]
    public async Task<Result<ActivityModel>> GetActivityById([FromRoute(Name = "id")]Guid id)
        => await Sender.Send(new ActivityRequest(id));
    [HttpGet("/my")]
    public async Task<Result<IEnumerable<ActivityModel>>> GetMyActivies()
        => await Sender.Send(new MyActivityRequest());
    [HttpGet]
    public async Task<Result<IEnumerable<ActivityModel>>> GetActivies()
        => await Sender.Send(new GetAllActiviesRequest());

    [HttpPost]
    public async Task<Result> AddAcrivity(CreateActivityRequest request)
        => await Sender.Send(request);

}