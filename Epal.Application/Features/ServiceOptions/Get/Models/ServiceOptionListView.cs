namespace Epal.Application.Features.ServiceOptions.Get.Models;

public record ServiceOptionListView(Guid Id, string Name, double Price, Guid ServiceTypeId);
