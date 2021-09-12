using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDbGeneration.Services;
using Microsoft.Extensions.Configuration;

namespace TestDbGeneration.IntegrationTest.Support.Services
{
    public class TestDataContext : DataContext
    {
        public TestDataContext(IConfiguration configuration)
            : base(configuration)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("Development"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
