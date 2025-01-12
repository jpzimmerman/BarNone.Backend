using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public interface IMenuItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("SpecialInstructions")]
        public string SpecialInstructions { get; set; }

        [JsonPropertyName("ingredients")]
        public List<string> Ingredients { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("numberOfOrders")]
        public uint NumberOfOrders { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

    }
}