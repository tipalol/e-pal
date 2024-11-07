using Epal.Domain.Entities;

namespace Epal.Application.Features.Services.ExtraInfo.Get.Model;

public class ServiceExtraInfoView
{
    public string? Rank { get; set; }
    public string? Photo { get; set; }
    public string[] Servers { get; set; } = [];
    public string[] Styles { get; set; } = [];
    public string[] Platforms { get; set; } = [];
    public string[] Positions { get; set; } = [];
    public ServiceExtraInfoView()
    {
    }
    public ServiceExtraInfoView(ServiceExtraInfo serviceExtraInfo)
    {
        Rank = serviceExtraInfo.Rank;
        Servers = serviceExtraInfo.Servers;
        Styles = serviceExtraInfo.Styles;
        Platforms = serviceExtraInfo.Platforms;
        Positions = serviceExtraInfo.Positions;
        Photo = serviceExtraInfo.Photo;
    }

   
}