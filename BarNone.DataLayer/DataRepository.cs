using BarNone.Models;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace BarNone.DataLayer
{
    public class DataRepository : IDataRepository
    {
        private readonly IDbConnection _connection;

        public DataRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public virtual async Task<DataTable> GetItems(string storedProcedureName)
        {
            try
            {
                using (var connection = new SqlConnection(_connection.ConnectionString))
                {
                    var command = new SqlCommand(storedProcedureName, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    using var reader = await command.ExecuteReaderAsync();
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic GetItems() error: {ex.Message}");
                return new DataTable();
            }
        }

        public virtual async Task AddItem(string storedProcedureName, Dictionary<string, object> parameters)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                var command = new MySqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Parallel.ForEach(parameters, paramKeyValuePair =>
                {
                    command.Parameters.AddWithValue(paramKeyValuePair.Key, paramKeyValuePair.Value);
                });

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
