﻿using System.Text.Json.Serialization;

namespace BarNone.Models
{
    public class MenuItem : IMenuItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new List<string>();
        public float Price { get; set; }
        public uint NumberOfOrders { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}