using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Epal.Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum UserStatus
{
    Created = 100,
    Confirmed = 101,
    Banned = 404
}