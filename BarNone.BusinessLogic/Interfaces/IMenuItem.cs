using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BarNone.BusinessLogic.Interfaces
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
