using System;
using System.Diagnostics;
using Infraestructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SeedDatabase(IHost host)
        {
            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NotePadContext>();
                if(context.Database.EnsureCreated())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    try
                    {
                        NotePadContextSeed.Initialize(context);
                        Debug.Print("A database seeding was initialize");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "A database seeding error ocurred.");
                    }
                }
            }
        }
    }
}
