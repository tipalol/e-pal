using Epal.Application.Common;
using Epal.Application.Features.Catalog.ServiceTypes.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Admin.ServiceType.Post;


public class Handler(IEpalDbContext context) : IRequestHandler<ServiceTypesDto, Result>
{
    public async Task<Result> Handle(ServiceTypesDto request, CancellationToken cancellationToken)
    {
        request.Id ??= Guid.NewGuid();
        var existedServiceType = await context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (existedServiceType is null)
        {
            await context.ServiceTypes.AddAsync(new Domain.Entities.ServiceType()
            {
                Name = request.Name,
                Avatar = request.Avatar,
                Description = request.Description
            }, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        else
        {
            existedServiceType.Name = request.Name;
            existedServiceType.Avatar = request.Avatar;
            existedServiceType.Description = request.Description;
            await context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
