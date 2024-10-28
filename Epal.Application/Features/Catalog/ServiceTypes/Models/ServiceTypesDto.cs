using Epal.Application.Common;
using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Catalog.ServiceTypes.Models;

public record ServiceTypesDto : IRequest<Result>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
}