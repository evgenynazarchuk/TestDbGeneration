﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TestDbGeneration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TestDbGeneration.Services;
using TestDbGeneration.IntegrationTest.Support.Services;
using Microsoft.EntityFrameworkCore;

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

                services.AddTransient<TestInitDataBase>();
                services.AddTransient<DataContext, TestDataContext>();

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                // set test schema
                // or generate test schema name
                var testSchema = this.GenerateRandomAlphanumericString();

                // set connection string
                var configuration = scopedServices.GetRequiredService<IConfiguration>();
                configuration["ConnectionStrings:Development"] = $"Server=localhost;Initial Catalog={testSchema};Trusted_Connection=True;";

                // create test schema
                var init = scopedServices.GetRequiredService<TestInitDataBase>();
                init.Database.ExecuteSqlRaw($"create database {testSchema}");

                // restore database to test schema
                // or create tables
                var db = scopedServices.GetRequiredService<DataContext>();
                db.Database.EnsureCreated();
            });
        }

        public string GenerateRandomAlphanumericString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
    }
}
