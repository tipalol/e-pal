using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Admin.ServiceTypes.Post;

public record AddOrUpdateCategoryRequest(Guid? Id, string Name, string Description, string Avatar) : IRequest<Result>;

public class Handler(IEpalDbContext context) : IRequestHandler<AddOrUpdateCategoryRequest, Result>
{
    public async Task<Result> Handle(AddOrUpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        if (request.Id.HasValue)
        {
            var existedServiceType = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (existedServiceType == null)
                return Result.Fail("Category not found");
            
            existedServiceType.Name = request.Name;
            existedServiceType.Avatar = request.Avatar;
            existedServiceType.Description = request.Description;
            
            await context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            await context.Categories.AddAsync(new Category
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
