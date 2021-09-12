using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestDbGeneration.IntegrationTest.Support.Services
{
    public class TestInitDataBase : DbContext
    {
        public IConfiguration configuration;

        public TestInitDataBase(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Trusted_Connection=True;");
        }
    }
}
