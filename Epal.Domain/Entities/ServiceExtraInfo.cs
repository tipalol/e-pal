using Epal.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Epal.Domain.Entities;

[PrimaryKey(nameof(ServiceId))]

public class ServiceExtraInfo
{
    public Service Service { get; set; }
    public Guid ServiceId { get; set; }
    public string? Photo { get; set; }
    public string? Rank { get; set; }
    public string[] Servers { get; set; } = [];
    public string[] Styles { get; set; } = [];
    public string[] Platforms { get; set; } = [];
    public string[] Positions { get; set; } = [];
}