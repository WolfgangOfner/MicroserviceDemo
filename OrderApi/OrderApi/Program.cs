using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OrderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false); // needed for mediatr DI
        }
    }
}