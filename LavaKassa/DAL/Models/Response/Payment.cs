using System.Text.Json.Serialization;

namespace LavaKassa.DAL.Models.Response
{
    public class Payment
    {
        [JsonPropertyName("data")]
        public PaymentData Data { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("status_check")]
        public bool StatusCheck { get; set; }
    }
}
