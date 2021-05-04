using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Messaging.Receive;

[assembly: FunctionsStartup(typeof(Startup))]

namespace OrderApi.Messaging.Receive
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");
            builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString));
        }
    }
}