using BarNone.Models;
using Moq;
using System.Data;
using Xunit;

namespace BarNone.DataLayer.Tests
{
    public class DataRepositoryTests : IDisposable
    {
        private bool disposedValue;
        private readonly Mock<IDbConnection> _connection = new Mock<IDbConnection>();

        public DataRepositoryTests()
        {
            _connection.Setup(m => m.ConnectionString).Returns("Server=sqlserver;Database=db;User ID=user;Password=pwd;");
        }

        [Fact()]
        public void DataRepositoryTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public async Task GetAllTest()
        {
            var dataRepository = new DataRepository(_connection.Object);

            var result = await dataRepository.GetItems(Constants.GetMenuItemsSp);

        }

        [Fact()]
        public void AddTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void DeleteTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void UpdateTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DataRepositoryTests()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}