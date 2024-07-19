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

            var command = new MySqlCommand(Constants.GetMenuItemsSp, (MySqlConnection)_connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            _connection.Open();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                menuItems.Add(new MenuItem
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetFloat(4),
                    Category = reader.GetString(6)
                });
            }
            _connection.Close();
            return menuItems;
        }

        public async Task AddGuestOrder(GuestOrder order)
        {
            var orderId = ulong.MinValue;

            var command = new MySqlCommand(Constants.AddGuestOrderSp, (MySqlConnection)_connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var orderIdCommand = new MySqlCommand("SELECT LAST_INSERT_ID()", (MySqlConnection)_connection) 
            { 
                CommandType = CommandType.Text 
            };

            command.Parameters.AddWithValue("@nameForOrder", order.Name);
            command.Parameters.AddWithValue("@total", order.Total);
            command.Parameters.AddWithValue("@specialInstructions", order.SpecialInstructions);

            try
            {
                _connection.Open();
                await command.ExecuteNonQueryAsync();
                orderId = (ulong)(await orderIdCommand.ExecuteScalarAsync());
            }
            finally
            {
                _connection.Close();
            }

            await AddOrderItems(orderId, order.Items);
        }

        private async Task AddOrderItems(ulong orderId, IEnumerable<IMenuItem> orderItems)
        {
            var bulkCopy = new MySqlBulkCopy((MySqlConnection)_connection)
            {
                DestinationTableName = "orders_cocktails",
            };
            var dataTable = new DataTable();
            dataTable.Columns.Add("ItemId", typeof(int));
            dataTable.Columns.Add("OrderId", typeof(int));
            dataTable.Columns.Add("DrinkId", typeof(int));
            dataTable.Columns.Add("SpecialInstructions", typeof(String));

            foreach (var item in orderItems) 
            {
                var itemCopy = dataTable.NewRow();
                itemCopy["OrderId"] = orderId;
                itemCopy["DrinkId"] = item.Id;
                itemCopy["SpecialInstructions"] = item.SpecialInstructions;
                dataTable.Rows.Add(itemCopy);
            }
            try
            {
                _connection.Open();
                await bulkCopy.WriteToServerAsync(dataTable);
            }
            finally
            {
                _connection.Close();
            }   
        }
    }
}
