namespace Epal.Application.Features.Services.ExtraInfo.AddOrUpdate.Models;

public record ServiceExtraInfoDto(Guid ServiceId, string? Rank, string? Photo, string[] Servers, string[] Styles, string[] Platforms, string[] Positions);
