using Infraestructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Web;

namespace FunctionalTests.Web
{
    public class WebTestFixture: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddEntityFrameworkInMemoryDatabase();

                //Create a new service provider.
                var provider = services
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider();

                //Add a database context (NotePadContext) using an in-memory
                services.AddDbContext<NotePadContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                //Build the service provider
                var sp = services.BuildServiceProvider();

                //Create a scope to obtain a reference to the database
                using(var scope = sp.CreateScope())
                {
                    var scoppedServices = scope.ServiceProvider;
                    var db = scoppedServices.GetRequiredService<NotePadContext>();

                    var logger = scoppedServices.GetRequiredService<ILogger<WebTestFixture>>();

                    //Ensure the database is created
                    db.Database.EnsureCreated();
                    try
                    {
                        NotePadContextSeed.Initialize(db);
                    }
                    catch(Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the " +
                            $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
            base.ConfigureWebHost(builder); 
        }
    }
}
