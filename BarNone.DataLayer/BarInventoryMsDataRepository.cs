using BarNone.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BarNone.DataLayer
{
    public class BarInventoryMsDataRepository : DataRepository, IBarInventoryDataRepository
    {
        private readonly IDbConnection _connection;

        public BarInventoryMsDataRepository(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Ingredient>> GetInventoryItems()
        {
            var inventoryItems = new List<Ingredient>();

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                var command = new SqlCommand(Constants.GetInventoryItemsSp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    inventoryItems.Add(new Ingredient
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Quantity = reader.GetInt32(3),
                        IsAlcoholic = reader.GetBoolean(4)
                    });
                }
            }

            return inventoryItems;
        }

        public async Task AddInventoryItem(Ingredient inputIngredient)
        { 
        
        }

        public async Task RemoveInventoryItem(Ingredient inputIngredient)
        { }
    }
}
