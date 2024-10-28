using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Admin.ServiceTypes.Post;

public record AddOrUpdateServiceTypeRequest(Guid? Id, string Name, string Description, string Avatar) : IRequest<Result>;

public class Handler(IEpalDbContext context) : IRequestHandler<AddOrUpdateServiceTypeRequest, Result>
{
    public async Task<Result> Handle(AddOrUpdateServiceTypeRequest request, CancellationToken cancellationToken)
    {
        if (request.Id.HasValue)
        {
            var existedServiceType = await context.ServiceTypes
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (existedServiceType == null)
                return Result.Fail("Service type not found");
            
            existedServiceType.Name = request.Name;
            existedServiceType.Avatar = request.Avatar;
            existedServiceType.Description = request.Description;
            
            await context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            await context.ServiceTypes.AddAsync(new ServiceType
            {
                Name = request.Name,
                Avatar = request.Avatar,
                Description = request.Description
            }, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        
        return Result.Ok();
    }
}
