using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarNone.BusinessLogic.Interfaces;

namespace BarNone.BusinessLogic.Models
{
    public class MenuItem : IMenuItem
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new List<string>();
        public float Price { get; set; }
        public uint NumberOfOrders {  get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
