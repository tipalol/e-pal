namespace Epal.Application.Features.Services.Get.Models;

public record ServiceListView(Guid Id, string Name, string Avatar, double Price, Guid ServiceTypeId);
