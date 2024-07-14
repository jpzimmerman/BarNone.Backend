using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public class GuestOrder
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("specialInstructions")]
        public string SpecialInstructions {  get; set; } = string.Empty;

        [JsonPropertyName("loyaltyProgramId")]
        public string LoyaltyProgramId { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public IEnumerable<IMenuItem> Items { get; set; } = Enumerable.Empty<IMenuItem>();

        [JsonPropertyName("Total")]
        public float Total {  get; set; }
    }
}
