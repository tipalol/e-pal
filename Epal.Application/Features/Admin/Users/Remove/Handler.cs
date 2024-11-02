using Epal.Application.Interfaces;
using MediatR;

namespace Epal.Application.Features.Admin.Users.Remove;

public record RemoveUserRequest(Guid Id) : IRequest;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<RemoveUserRequest>
{
    public async Task Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Profiles.FindAsync(request.Id);
        if (user == null)
        {
            throw new ArgumentException($"Пользователь с ID {request.Id} не найден");
        }
        context.Profiles.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}