using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDbGeneration.Models;
using TestDbGeneration.IntegrationTest.Support.Services;

namespace TestDbGeneration.IntegrationTest.Support.Helpers
{
    public class InitializeData
    {
        public static void CreateDefaultData(TestDataContext db)
        {
            db.Set<Person>().Add(new Person
            {
                Name = DefaultConstants.PersonName
            });

            db.Set<Location>().Add(new Location
            {
                Name = DefaultConstants.LocationName
            });

            db.SaveChanges();
        }
    }
}
