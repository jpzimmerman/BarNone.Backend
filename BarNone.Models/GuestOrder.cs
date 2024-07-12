using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public class GuestOrder
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        private string Name { get; set; } = string.Empty;

        [JsonPropertyName("loyaltyProgramId")]
        private string LoyaltyProgramId { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        private IEnumerable<IMenuItem> Items { get; set; } = Enumerable.Empty<IMenuItem>();

        [JsonPropertyName("Total")]
        private float Total {  get; set; }
    }
}
