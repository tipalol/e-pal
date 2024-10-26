using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Epal.Domain.Enums;

public enum UserStatus
{
    [Description("Создан")]
    Created = 100,
    [Description("Подтвержден")]
    Confirmed = 101,
    [Description("Забанен")]
    Banned = 404
}