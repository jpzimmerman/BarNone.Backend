using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace BarNone.DataLayer
{
    public class DataRepository<T> : IDataRepository<T>
    {
        public IDbConnection Connection { get; private set; }

        public DataRepository(IDbConnection connection) 
        {
            Connection = connection;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (Connection) 
            {
                Connection.Open();
            }
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
        }

        public void Delete(T item)
        {
        }

        public void Update(T item)
        {
        }
    }
}
