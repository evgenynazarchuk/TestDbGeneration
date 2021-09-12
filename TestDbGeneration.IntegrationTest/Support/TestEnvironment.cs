using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using TestDbGeneration.Services;

namespace TestDbGeneration.IntegrationTest.Support
{
    public class TestEnvironment : IDisposable
    {
        public TestApplication WebApp;

        public HttpClient HttpClient;

        public TestEnvironment()
        {
            this.WebApp = new();
            this.HttpClient = this.WebApp.CreateClient();
        }

        public void Dispose()
        {
            //// drop tables and database
            var dataContext = this.WebApp.Server.Services.GetRequiredService<DataContext>();
            dataContext.Database.EnsureDeleted();
        }
    }
}
