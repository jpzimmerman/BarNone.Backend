using BarNone.Models;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System.Data;

namespace BarNone.DataLayer
{
    public class MenuMsDataRepository : DataRepository, IMenuDataRepository
    {
        private readonly IDbConnection _connection;

        public MenuMsDataRepository(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task AddGuestOrder(GuestOrder order)
        {
            var orderId = ulong.MinValue;
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                var command = new SqlCommand(Constants.AddGuestOrderSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var orderIdCommand = new SqlCommand("SELECT IDENT_CURRENT('orders')", connection)
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
                    orderId = Convert.ToUInt64(await orderIdCommand.ExecuteScalarAsync());
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
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                var bulkCopy = new SqlBulkCopy(connection)
                {
                    DestinationTableName = "orders_cocktails",
                };
                var dataTable = new DataTable();
                dataTable.Columns.Add("ItemId", typeof(int));
                dataTable.Columns.Add("OrderId", typeof(int));
                dataTable.Columns.Add("DrinkId", typeof(int));
                dataTable.Columns.Add("Quantity", typeof(int));
                dataTable.Columns.Add("SpecialInstructions", typeof(String));
                Parallel.ForEach(orderItems, item =>
                {
                    var itemCopy = dataTable.NewRow();
                    itemCopy["OrderId"] = orderId;
                    itemCopy["DrinkId"] = item.Id;
                    itemCopy["Quantity"] = item.Quantity;
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

        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems()
        {
            var menuItems = new List<IMenuItem>();

            try
            {
                var dataTable = await base.GetItems(Constants.GetMenuItemsSp);
                menuItems = (from item in dataTable.AsEnumerable()
                             select new MenuItem()
                             {
                                 Id = Convert.ToInt32(item["Id"]),
                                 Name = item["Name"].ToString() ?? string.Empty,
                                 Description = item["Description"].ToString() ?? string.Empty,
                                 Price = (float)Convert.ToDouble(item["Price"]),
                                 Category = item["Category"].ToString() ?? string.Empty,
                                 Tags = []
                             }).ToList<IMenuItem>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllMenuItems() error: {ex.Message}");
            }

            return menuItems;
        }

        public async Task<IEnumerable<TagCocktailMapItem>> GetTagCocktailMap()
        {
            var tagsCocktailsEntries = new List<TagCocktailMapItem>();
            try
            {
                var dataTable = await base.GetItems(Constants.GetTagCocktailMapSp);
                tagsCocktailsEntries = (from item in dataTable.AsEnumerable()
                             select new TagCocktailMapItem()
                             {
                                 TagId = Convert.ToInt32(item["TagId"]),
                                 DrinkId = Convert.ToInt32(item["TagId"]),
                                 TagName = item["TagName"].ToString() ?? string.Empty
                             }).ToList<TagCocktailMapItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTagCocktailMap() error: {ex.Message}");
            }

            return tagsCocktailsEntries;
        }

        public async Task<IEnumerable<string>> GetTags()
        {
            var tagList = new List<String>();
            try
            {
                var dataTable = await base.GetItems(Constants.GetTagsSp);
                tagList = dataTable.AsEnumerable()
                    .Select(item => item.Field<String>("TagName") ?? string.Empty)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTags() error: {ex.Message}");
            }

            return tagList;
        }
    }
}
