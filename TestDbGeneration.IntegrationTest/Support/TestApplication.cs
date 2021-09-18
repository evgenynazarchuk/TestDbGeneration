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
                // сгенерировать тестовое имя базы данных
                var testDatabase = StringHelper.GenerateString();

                // создать базу данных (без таблиц)
                using (var baseDatabaseConnect = new InitializeDatabase())
                {
                    baseDatabaseConnect.CreateDatabase(testDatabase);
                }

                // найти оригинальный коннект к базе и заменить на свой
                var dataContext = services.Single(d => d.ServiceType == typeof(DataContext));
                services.Remove(dataContext);
                services.AddTransient<DataContext>(x => new TestDataContext($"Server=localhost;Database={testDatabase};Trusted_Connection=True;"));

                // получить ссылку на базу данных
                var sp = services.BuildServiceProvider();
                // using var scope = sp.CreateScope();
                // var serviceProdiver = scope.ServiceProvider;
                var dbConnect = sp.GetRequiredService<DataContext>() as TestDataContext;

                // создать таблицы
                dbConnect.Database.EnsureCreated();
                // созданные данные в таблицах
                InitializeData.CreateDefaultData(dbConnect);
            });
        }
    }
}
