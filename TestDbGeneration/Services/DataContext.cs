using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestDbGeneration.Models;

namespace TestDbGeneration.Services
{
    public class DataContext : DbContext
    {
        public IConfiguration configuration;

        public DataContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=AWPHPVGVDZ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Location>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
