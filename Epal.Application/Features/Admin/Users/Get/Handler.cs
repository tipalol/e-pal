using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Admin.Users.Get;

public record GetUserRequest(Guid Id) : IRequest<Domain.Entities.Profile>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetUserRequest, Domain.Entities.Profile>
{
    public async Task<Domain.Entities.Profile> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = context.Profiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return await user ?? throw new ArgumentException($"Пользователь с ID {request.Id} не найден");
    }
}