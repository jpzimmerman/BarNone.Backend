using BarNone.Models;

namespace BarNone.DataLayer
{
    public interface IDataRepository
    {
        Task<IEnumerable<IMenuItem>> GetAllMenuItems();
        Task<IEnumerable<TagCocktailMapItem>> GetTagCocktailMap();
        Task AddGuestOrder(GuestOrder order);
        Task<IEnumerable<string>> GetTags();
        Task AddItem(string storedProcedureName, Dictionary<string, object> parameters);
    }
}
