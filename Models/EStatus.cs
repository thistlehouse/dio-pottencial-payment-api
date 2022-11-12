using System.Text.Json.Serialization;

namespace tech_test_payment_api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EStatus
    {
        Awaiting_Payment = 0,
        Payment_Approved = 1,               
        Cancelled = 2,
        Shipping = 3,
        Delivered = 4
    }
}