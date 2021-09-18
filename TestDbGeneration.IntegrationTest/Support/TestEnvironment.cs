using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using TestDbGeneration.Services;
using TestDbGeneration.IntegrationTest.Support.Helpers.Facade;

namespace TestDbGeneration.IntegrationTest.Support
{
    public class TestEnvironment : IDisposable
    {
        public TestApplication WebApp;

        public HttpClient HttpClient;

        public PersonFacade PersonFacade;

        public LocationFacade LocationFacade;

        public TestEnvironment()
        {
            this.WebApp = new();
            this.HttpClient = this.WebApp.CreateClient();
            this.PersonFacade = new(this.HttpClient);
            this.LocationFacade = new(this.HttpClient);
        }

        public void Dispose()
        {
            // удалить базу данных и таблицы
            var dataContext = this.WebApp.Server.Services.GetRequiredService<DataContext>();
            dataContext.Database.EnsureDeleted();
        }
    }
}
