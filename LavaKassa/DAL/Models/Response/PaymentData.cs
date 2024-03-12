using LavaKassa.BLL.Converters;
using System.Text.Json.Serialization;

namespace LavaKassa.DAL.Models.Response
{
    public class PaymentData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("expired")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Expired { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("shop_id")]
        public string ShopId { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("fail_url")]
        public string FailUrl { get; set; }

        [JsonPropertyName("success_url")]
        public string SuccessUrl { get; set; }

        [JsonPropertyName("hook_url")]
        public string HookUrl { get; set; }

        [JsonPropertyName("custom_fields")]
        public string CustomFields { get; set; }

        [JsonPropertyName("merchantName")]
        public string MerchantName { get; set; }

        [JsonPropertyName("exclude_service")]
        public string ExcludeService { get; set; }

        [JsonPropertyName("include_service")]
        public List<string> IncludeService { get; set; }
    }
}
