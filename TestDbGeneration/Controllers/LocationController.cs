using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbGeneration.Services;
using TestDbGeneration.Models;

namespace TestDbGeneration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        protected readonly DataContext dataContext;

        public LocationController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations = this.dataContext.Set<Location>().ToArray();
            return Ok(locations);
        }

        [HttpPost]
        public IActionResult Post(Location location)
        {
            this.dataContext.Set<Location>().Add(location);
            this.dataContext.SaveChanges();
            return Ok(location);
        }

        [HttpPut]
        public IActionResult Put(Location location)
        {
            this.dataContext.Set<Location>().Update(location);
            this.dataContext.SaveChanges();
            return Ok(location);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var location = this.dataContext.Set<Location>().Find(id);
            this.dataContext.Set<Location>().Remove(location);
            this.dataContext.SaveChanges();
            return Ok(location);
        }
    }
}
