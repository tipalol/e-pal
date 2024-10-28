﻿using Epal.Application.Common;
using Epal.Application.Features.Activity.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Activity.Get;


public record ActivityRequest(Guid Id) : IRequest<Result<ActivityModel>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<ActivityRequest, Result<ActivityModel>>
{
    public async Task<Result<ActivityModel>> Handle(ActivityRequest request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return activity is null ?  Result<ActivityModel>.Fail("Profile not found") : Result<ActivityModel>.Ok(new ActivityModel(activity));
    }
}