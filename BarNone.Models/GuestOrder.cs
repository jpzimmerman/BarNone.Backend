using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public class GuestOrder
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("specialInstructions")]
        public string SpecialInstructions {  get; set; } = string.Empty;

        [JsonPropertyName("loyaltyProgramId")]
        public string LoyaltyProgramId { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();

        [JsonPropertyName("total")]
        public float Total {  get; set; }
    }
}
