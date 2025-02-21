using BarNone.Models;

namespace BarNone.DataLayer
{
    public interface IBarInventoryDataRepository
    {
        public Task<IEnumerable<Ingredient>> GetInventoryItems();
        Task AddInventoryItem(Ingredient inputIngredient);
        Task RemoveInventoryItem(Ingredient inputIngredient);
    }
}
