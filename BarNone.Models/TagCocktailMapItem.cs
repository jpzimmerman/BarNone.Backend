using System.Diagnostics.CodeAnalysis;

namespace BarNone.Models
{
    [ExcludeFromCodeCoverage]
    public class TagCocktailMapItem
    {
        public int TagId {  get; set; }
        public int DrinkId {  get; set; }
        public string TagName { get; set; } = string.Empty;
    }
}
