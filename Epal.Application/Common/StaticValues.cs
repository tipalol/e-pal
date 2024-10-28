using Epal.Domain.Enums;

namespace Epal.Application.Common;

public static class StaticValues
{
    public static string DefaultAvatarByGender(Gender gender)
    {
        return gender switch
        {
            Gender.Man => "https://i.postimg.cc/Z5cJF68J/men.webp?x-oss-process=image/resize,m_fill,w_256,h_256",
            Gender.Woman => "https://i.postimg.cc/ryBy8Vy2/woman.webp?x-oss-process=image/resize,m_fill,w_256,h_256",
            Gender.Unselected =>
                "https://th.bing.com/th/id/R.6eec4aaf95a7775913960d599b47eec8?rik=pqGASt3uwgtBnw&pid=ImgRaw&r=0?x-oss-process=image/resize,m_fill,w_256,h_256",
            _ => ""
        };
    }
}