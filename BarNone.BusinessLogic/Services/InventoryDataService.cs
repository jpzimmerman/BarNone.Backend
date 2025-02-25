using BarNone.BusinessLogic.Builders;
using BarNone.DataLayer;
using BarNone.Models;

namespace BarNone.BusinessLogic.Services
{
    public class InventoryDataService
    {
        private readonly BarInventoryMsDataRepository _dataRepository;

        public InventoryDataService(BarInventoryMsDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IEnumerable<Ingredient>> GetInventoryItems()
        {
            var menuItems = await _dataRepository.GetInventoryItems();
            return menuItems;
        }

        public async Task AddInventoryItem(Ingredient item)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@itemName", item.Name},
                {"@itemDescription", item.Description},
                {"@isAlcoholic", item.IsAlcoholic}
            };
            await _dataRepository.AddItem(Constants.AddInventoryItemSp, parameters);
        }
    }
}
