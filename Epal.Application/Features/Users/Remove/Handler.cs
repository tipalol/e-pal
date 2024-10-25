using System.Xml.Schema;
using Epal.Application.Interfaces;
using MediatR;

namespace Epal.Application.Features.Users.Remove;

public record RemoveUserRequest(Guid Id) : IRequest;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<RemoveUserRequest>
{
    public async Task Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id);
        if (user == null)
        {
            throw new ArgumentException($"Пользователь с ID {request.Id} не найден");
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}