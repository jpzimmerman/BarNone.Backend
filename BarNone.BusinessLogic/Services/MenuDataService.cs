using BarNone.DataLayer;
using BarNone.Models;
using MySqlConnector;
using System.Data;
using System.Runtime.InteropServices;

namespace BarNone.BusinessLogic.Services
{
    public class MenuDataService
    {
        private readonly IDataRepository _dataRepository;

        public MenuDataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems() => await _dataRepository.GetAllMenuItems();

        public async Task AddOrder(GuestOrder order) => await _dataRepository.AddGuestOrder(order);

        public async Task<IEnumerable<string>> GetTags() => await _dataRepository.GetTags();

        public async Task AddInventoryItem(Ingredient item)
        {
            var parameters = new Dictionary<string, object>() 
            {
                {"@name", item.Name},
                {"@description", item.Description},
                {"@isAlcoholic", item.IsAlcoholic}
            };
            await _dataRepository.AddItem(Constants.AddGuestOrderSp, parameters);

        }
    }
}
