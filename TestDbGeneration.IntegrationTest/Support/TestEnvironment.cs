using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDbGeneration.IntegrationTest.Support;
using System.Net.Http;
using TestDbGeneration.IntegrationTest.Support.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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
            //// drop tables and schema
            var dataContext = this.WebApp.Server.Services.GetRequiredService<DataContext>();
            dataContext.Database.EnsureDeleted();
        }
    }
}
