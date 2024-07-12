using BarNone.Models;
using MySqlConnector;
using System.Data;

namespace BarNone.DataLayer
{
    public class DataRepository : IDataRepository
    {
        private readonly IDbConnection _connection;

        public DataRepository(IDbConnection connection) 
        {
            _connection = connection;
        }

        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems()
        {
            var menuItems = new List<IMenuItem>();

            _connection.Open();
            var command = new MySqlCommand("GetMenuItems", (MySqlConnection)_connection);
            command.CommandType = CommandType.StoredProcedure;
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
            _connection.Close();
            return menuItems;
        }

        public async Task AddGuestOrder(GuestOrder order)
        {
            _connection.Open();
            _connection.Close();
        }
    }
}
