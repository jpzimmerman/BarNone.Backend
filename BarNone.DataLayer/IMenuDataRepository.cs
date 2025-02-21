using BarNone.Models;

namespace BarNone.DataLayer
{
    public interface IMenuDataRepository
    {
        Task<IEnumerable<IMenuItem>> GetAllMenuItems();
        Task<IEnumerable<TagCocktailMapItem>> GetTagCocktailMap();
        Task AddGuestOrder(GuestOrder order);
        Task<IEnumerable<string>> GetTags();
    }
}
