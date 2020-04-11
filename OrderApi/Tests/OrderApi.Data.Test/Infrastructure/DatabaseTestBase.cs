using System;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Database;

namespace OrderApi.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly OrderContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<OrderContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new OrderContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}