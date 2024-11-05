namespace Epal.Application.Features.ServiceOptions.Get.Models;

public record ServiceOptionListView(Guid Id, string Name, string Description, double Price, Guid ServiceTypeId);
