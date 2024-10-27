using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Epal.Domain.Enums;

public enum UserStatus
{
    [JsonConverter(typeof(StringEnumConverter))]
    Created = 100,
    [JsonConverter(typeof(StringEnumConverter))]
    Confirmed = 101,
    [JsonConverter(typeof(StringEnumConverter))]
    Banned = 404
}