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
using Microsoft.AspNetCore.TestHost;

namespace TestDbGeneration.IntegrationTest.Support
{
    public class TestApplication : WebApplicationFactory<Startup>, IDisposable
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSolutionRelativeContentRoot("");

            builder.ConfigureServices(services =>
            {
                // сгенерировать тестовое имя базы данных
                var testDatabaseName = StringHelper.GenerateString();
                var masterConnectionString = $"Server=localhost;Trusted_Connection=True;";
                var testConnectionString = $"Server=localhost;Database={testDatabaseName};Trusted_Connection=True;";

                // создать базу данных (без таблиц)
                using (var masterDatabaseContext = new TestDataContext(masterConnectionString))
                {
                    InitializeData.CreateDatabase(masterDatabaseContext, testDatabaseName);
                }

                // найти оригинальный коннект к базе и заменить на свой
                var originalDatabaseContext = services.Single(d => d.ServiceType == typeof(DataContext));
                services.Remove(originalDatabaseContext);
                services.AddTransient<DataContext>(x => new TestDataContext(testConnectionString));

                // получить новый коннект на базу данных
                var sp = services.BuildServiceProvider();
                var dbConnect = sp.GetRequiredService<DataContext>() as TestDataContext;
                // создать таблицы
                dbConnect.Database.EnsureCreated();
                // созданные данные в таблицах
                InitializeData.CreateDefaultData(dbConnect);
            });
        }
    }
}
