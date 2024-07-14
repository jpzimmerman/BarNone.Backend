using BarNone.Models;

namespace BarNone.DataLayer
{
    public interface IDataRepository
    {
        Task<IEnumerable<IMenuItem>> GetAllMenuItems();
        Task AddGuestOrder(GuestOrder order);
        Task AddOrderItems(IEnumerable<IMenuItem> orderItems);
    }
}
