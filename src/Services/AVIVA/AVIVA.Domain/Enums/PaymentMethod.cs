using System.Text.Json.Serialization;

namespace AVIVA.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMode
    {
        Cash = 0,
        Card = 1,
        Transfer = 2
    }
}
