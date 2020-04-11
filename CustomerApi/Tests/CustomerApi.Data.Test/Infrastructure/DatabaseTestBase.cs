using System;
using CustomerApi.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly CustomerContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new CustomerContext(options);

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