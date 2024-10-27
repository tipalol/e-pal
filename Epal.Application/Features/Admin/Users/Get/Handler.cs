using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Admin.Users.Get;

public record GetUserRequest(Guid Id) : IRequest<User>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetUserRequest, User>
{
    public async Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return await user ?? throw new ArgumentException($"Пользователь с ID {request.Id} не найден");
    }
}