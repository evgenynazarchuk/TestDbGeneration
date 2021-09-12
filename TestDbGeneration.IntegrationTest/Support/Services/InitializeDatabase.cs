using Microsoft.EntityFrameworkCore;

namespace TestDbGeneration.IntegrationTest.Support.Services
{
    public class InitializeDatabase : DbContext
    {
        public InitializeDatabase() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Trusted_Connection=True;");
        }
    }
}
