using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace BarNone.DataLayer
{
    public class DataRepository
    {
        private readonly IDbConnection _connection;

        public DataRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public virtual async Task AddItem(string storedProcedureName, Dictionary<string, object> parameters)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (var paramKeyValuePair in parameters)
                {
                    command.Parameters.AddWithValue(paramKeyValuePair.Key, paramKeyValuePair.Value);
                }

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AddItem() (procedure name: {storedProcedureName}) error: {ex.Message}");
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
