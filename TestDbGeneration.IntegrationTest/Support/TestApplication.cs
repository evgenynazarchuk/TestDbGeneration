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

                services.AddTransient<DataContext, TestDataContext>();

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                // generate test database name
                var testSchema = StringHelper.GenerateString();

                // set test connection string
                var configuration = scopedServices.GetRequiredService<IConfiguration>();
                configuration["ConnectionStrings:Development"] = $"Server=localhost;Initial Catalog={testSchema};Trusted_Connection=True;";

                // create test database
                using var init = new InitializeDatabase();
                init.Database.ExecuteSqlRaw($"create database {testSchema}");

                // TODO: restore database from backup
                // or generates tables
                var db = scopedServices.GetRequiredService<DataContext>();
                db.Database.EnsureCreated();

                // TODO: initialize data tables 
                //
            });
        }
    }
}
