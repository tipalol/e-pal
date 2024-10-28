using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Epal.Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ProfileType
{
    User = 100,
    Epal = 101,
    Admin = 666
}
