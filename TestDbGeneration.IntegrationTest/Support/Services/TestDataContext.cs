using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestDbGeneration.Services;

namespace TestDbGeneration.IntegrationTest.Support.Services
{
    public class TestDataContext : DataContext
    {
        protected string connectionString = string.Empty;

        public TestDataContext(string connectionString)
            : base(new ConfigurationBuilder().Build())
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
