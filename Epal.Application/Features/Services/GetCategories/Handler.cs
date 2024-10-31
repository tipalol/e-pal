using Epal.Application.Common;
using Epal.Application.Features.Services.GetCategories.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.GetCategories;

public record GetCategoriesRequest(Guid ProfileId) : IRequest<Result<IEnumerable<CategoryListViewWithPrice>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetCategoriesRequest, Result<IEnumerable<CategoryListViewWithPrice>>>
{
    public async Task<Result<IEnumerable<CategoryListViewWithPrice>>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Users
            .Include(x => x.Services)
            .ThenInclude(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);

        if (profile == null)
            return Result<IEnumerable<CategoryListViewWithPrice>>.Fail("User not found");

        var serviceTypes = profile.Services
            .GroupBy(x => x.Category.Id) // Сгруппируйте сервисы по CategoryId
            .Select(group => new CategoryListViewWithPrice(
                group.Key, // Id категории
                group.First().Category.Name, // Имя категории
                group.First().Category.Avatar, // Аватар категории
                group.Min(s => s.Price) // Минимальная цена для этой категории
            ))
            .ToArray(); 

        return Result<IEnumerable<CategoryListViewWithPrice>>.Ok(serviceTypes);
    }
}
