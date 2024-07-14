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
            var command = new MySqlCommand("GetMenuItems", (MySqlConnection)_connection) 
            {
                CommandType = CommandType.StoredProcedure
            };
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
            var command = new MySqlCommand("AddGuestOrder", (MySqlConnection)_connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@nameForOrder", order.Name);
            command.Parameters.AddWithValue("@total", order.Total);
            command.Parameters.AddWithValue("@specialInstructions", order.SpecialInstructions);
            _connection.Open();
            var result = await command.ExecuteNonQueryAsync();
            _connection.Close();
        }

        public Task AddOrderItems(IEnumerable<IMenuItem> orderItems)
        {
            // TODO: use bulk add to add order items to map table in DB 
            throw new NotImplementedException();
        }
    }
}
