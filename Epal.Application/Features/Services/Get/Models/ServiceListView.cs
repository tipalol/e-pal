namespace Epal.Application.Features.Services.Get.Models;

public record ServiceListView(Guid Id, string Name, double Price, string Icon, string description, Guid CategoryId);
