using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public interface IMenuItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("ingredients")]
        public List<string> Ingredients { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("numberOfOrders")]
        public uint NumberOfOrders { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }
    }
}