using BarNone.BusinessLogic.Interfaces;
using BarNone.BusinessLogic.Models;
using BarNone.DataLayer;
using MySqlConnector;
using System.Data;
using System.Security.Cryptography;

namespace BarNone.BusinessLogic.Services
{
    public class MenuDataService
    {
        private readonly IDataRepository<IMenuItem> _dataRepository;

        public MenuDataService(IDataRepository<IMenuItem> dataRepository) 
        {
            _dataRepository = dataRepository;
        }
        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems()
        { 
            var menuItems = new List<IMenuItem>();

            using (var connection = new MySqlConnection(_dataRepository.Connection.ConnectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM drinks;", connection);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    menuItems.Add(new MenuItem
                    {
                        Name = (string)reader.GetValue(1),
                        Description = (string)reader.GetValue(2),
                        Category = (string)reader.GetValue(6)
                    });
                }
            }
            return menuItems;
        }

        public async Task AddMenuItemsToOrder(IMenuItem menuItem)
        {
            using (var connection = new MySqlConnection(_dataRepository.Connection.ConnectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO cocktails.drinks (Name, Description, IngredientIDs, Category) VALUES(@name, @description, '', @Category)", connection);
                command.Parameters.Add(new MySqlParameter("@name", menuItem.Name));
                command.Parameters.Add(new MySqlParameter("@description", menuItem.Description));
                command.Parameters.Add(new MySqlParameter("@category", menuItem.Category));
                using var reader = await command.ExecuteReaderAsync();
            }
            
        }
    }
}
