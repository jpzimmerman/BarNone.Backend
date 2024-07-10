using System.Data;

namespace BarNone.DataLayer
{
    public interface IDataRepository<T>
    {
        public IDbConnection Connection { get; }
        Task<IEnumerable<T>> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
