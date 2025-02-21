﻿using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public class Ingredient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("quantity")]
        public uint Quantity { get; set; }

        [JsonPropertyName("isAlcoholic")]
        public bool IsAlcoholic { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; } = string.Empty;
    }
}
