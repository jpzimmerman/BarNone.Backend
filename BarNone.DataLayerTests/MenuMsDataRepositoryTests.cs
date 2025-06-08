using Xunit;
using Moq;
using BarNone.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BarNone.DataLayer.Tests
{
    public class MenuMsDataRepositoryTests : IDisposable
    {
        private Mock<IDbConnection> _connection = new Mock<IDbConnection>();
        private bool disposedValue;

        public MenuMsDataRepositoryTests()
        {
            _connection.Setup(x => x.ConnectionString).Returns("valid_string");          
        }


        [Fact()]
        public void MenuMsDataRepositoryTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void AddGuestOrderTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void GetAllMenuItemsTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void GetTagCocktailMapTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void GetTagsTest()
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
        // ~MenuMsDataRepositoryTests()
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