using BarNone.Models;
using MySqlConnector;
using Mysqlx.Crud;
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

            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(Constants.GetMenuItemsSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
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
            }

            return menuItems;
        }

        public async Task AddGuestOrder(GuestOrder order)
        {
            var orderId = ulong.MinValue;
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(Constants.AddGuestOrderSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var orderIdCommand = new MySqlCommand("SELECT LAST_INSERT_ID()", connection)
                {
                    CommandType = CommandType.Text
                };

                command.Parameters.AddWithValue("@nameForOrder", order.Name);
                command.Parameters.AddWithValue("@total", order.Total);
                command.Parameters.AddWithValue("@specialInstructions", order.SpecialInstructions);
                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    orderId = (ulong)(await orderIdCommand.ExecuteScalarAsync());
                }
                catch (Exception ex)
                { 
                    Console.WriteLine($"AddGuestOrder() error: {ex.Message}");
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            await AddOrderItems(orderId, order.Items);
        }

        private async Task AddOrderItems(ulong orderId, IEnumerable<IMenuItem> orderItems)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var bulkCopy = new MySqlBulkCopy(connection)
                {
                    DestinationTableName = "orders_cocktails",
                };
                var dataTable = new DataTable();
                dataTable.Columns.Add("ItemId", typeof(int));
                dataTable.Columns.Add("OrderId", typeof(int));
                dataTable.Columns.Add("DrinkId", typeof(int));
                dataTable.Columns.Add("SpecialInstructions", typeof(String));
                Parallel.ForEach(orderItems, item =>
                {
                    var itemCopy = dataTable.NewRow();
                    itemCopy["OrderId"] = orderId;
                    itemCopy["DrinkId"] = item.Id;
                    itemCopy["SpecialInstructions"] = item.SpecialInstructions;
                    dataTable.Rows.Add(itemCopy);
                });
                try
                {
                    connection.Open();
                    await bulkCopy.WriteToServerAsync(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AddOrderItems() error: {ex.Message}");
                }
                finally 
                {
                    await connection.CloseAsync();
                }
            }

        }

        public async Task<IEnumerable<string>> GetTags()
        {
            var menuItems = new List<string>();

            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(Constants.GetTagsSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    connection.Open();
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        menuItems.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GetTags() error: {ex.Message}");
                }
                finally 
                {
                    await connection.CloseAsync();
                }
            }

            return menuItems;
        }

        public async Task AddInventoryItem(Ingredient item)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(Constants.AddGuestOrderSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@isAlcoholic", item.IsAlcoholic);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AddInventoryItem() error: {ex.Message}");
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
