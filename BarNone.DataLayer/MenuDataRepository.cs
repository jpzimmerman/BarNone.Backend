﻿using BarNone.Models;
using MySqlConnector;
using System.Data;

namespace BarNone.DataLayer
{
    public class MenuDataRepository : DataRepository, IMenuDataRepository
    {
        private readonly IDbConnection _connection;

        public MenuDataRepository(IDbConnection connection) : base(connection)
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
                try
                {
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
                            Category = reader.GetString(6),
                            Tags = ["Sweet", "Citrusy"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GetAllMenuItems() error: {ex.Message}");
                }
                finally 
                {
                    await connection.CloseAsync();
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
                    orderId = (ulong)await orderIdCommand.ExecuteScalarAsync();
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
            var tagList = new List<string>();

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
                        tagList.Add(reader.GetString(0));
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
            return tagList;

        }

        public async Task<IEnumerable<TagCocktailMapItem>> GetTagCocktailMap()
        {
            var tagsCocktailsEntries = new List<TagCocktailMapItem>();

            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(Constants.GetTagCocktailMapSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    connection.Open();
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        tagsCocktailsEntries.Add(new TagCocktailMapItem
                        {
                            TagId = reader.GetInt32(0),
                            DrinkId = reader.GetInt32(1),
                            TagName = reader.GetString(2)
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GetTagsCocktailsMap() error: {ex.Message}");
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return tagsCocktailsEntries;
        }
    }
}
