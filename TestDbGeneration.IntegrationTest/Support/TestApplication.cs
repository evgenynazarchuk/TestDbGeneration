using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TestDbGeneration.IntegrationTest.Support.Helpers;
using TestDbGeneration.IntegrationTest.Support.Services;
using TestDbGeneration.Services;

namespace TestDbGeneration.IntegrationTest.Support
{
    public class TestApplication : WebApplicationFactory<Startup>, IDisposable
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dataContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DataContext));
                services.Remove(dataContext);

                //services.AddTransient<InitDataBase>();
                services.AddTransient<DataContext, TestDataContext>();

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                // set test schema
                // or generate test schema name
                var testSchema = StringHelper.GenerateString();

                // set connection string
                var configuration = scopedServices.GetRequiredService<IConfiguration>();
                configuration["ConnectionStrings:Development"] = $"Server=localhost;Initial Catalog={testSchema};Trusted_Connection=True;";

                // create test schema
                //var init = scopedServices.GetRequiredService<InitDataBase>();
                using var init = new InitializeDatabase();
                init.Database.ExecuteSqlRaw($"create database {testSchema}");

                // restore database to test schema
                // or create tables
                var db = scopedServices.GetRequiredService<DataContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
