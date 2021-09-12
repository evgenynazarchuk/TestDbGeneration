using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDbGeneration.Models;

namespace TestDbGeneration.IntegrationTest.Support.Helpers
{
    public class InitializeData
    {
        public static void CreateDefaultData(DbContext db)
        {
            db.Set<Person>().Add(new Person
            {
                Name = "Person Default Name"
            });

            db.Set<Location>().Add(new Location
            {
                Name = "Location Default Name"
            });
        }
    }
}
