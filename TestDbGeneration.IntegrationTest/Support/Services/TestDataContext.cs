using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestDbGeneration.Services;

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
