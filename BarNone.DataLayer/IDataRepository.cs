﻿using BarNone.Models;

namespace BarNone.DataLayer
{
    public interface IDataRepository
    {
        Task<IEnumerable<IMenuItem>> GetAllMenuItems();
        Task AddGuestOrder(GuestOrder order);
        Task<IEnumerable<string>> GetTags();
        Task AddItem(string storedProcedureName, Dictionary<string, object> parameters);
    }
}
