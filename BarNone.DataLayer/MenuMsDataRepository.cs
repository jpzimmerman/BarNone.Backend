using BarNone.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BarNone.DataLayer
{
    public class MenuMsDataRepository : IMenuDataRepository
    {
        private readonly IDbConnection _connection;

        public MenuMsDataRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task AddGuestOrder(GuestOrder order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems()
        {
            var menuItems = new List<MenuItem>();

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                var command = new SqlCommand(Constants.GetMenuItemsSp, connection)
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
                        Price = (float)reader.GetDouble(4),
                        Category = reader.GetString(6),
                        Tags = ["Sweet", "Citrusy"]
                    });
                }
            }

            return menuItems;
        }

        public async Task<IEnumerable<TagCocktailMapItem>> GetTagCocktailMap()
        {
            var tagsCocktailsEntries = new List<TagCocktailMapItem>();

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                var command = new SqlCommand(Constants.GetTagCocktailMapSp, connection)
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

        public async Task<IEnumerable<string>> GetTags()
        {
            var tagList = new List<string>();

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {

                var command = new SqlCommand(Constants.GetTagsSp, connection)
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
    }
}
