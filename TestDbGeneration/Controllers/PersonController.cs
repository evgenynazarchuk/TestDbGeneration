using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TestDbGeneration.Models;
using TestDbGeneration.Services;

namespace TestDbGeneration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        protected readonly DataContext dataContext;

        public PersonController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(2000);
            var persons = this.dataContext.Set<Person>().ToArray();
            return Ok(persons);
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            this.dataContext.Set<Person>().Add(person);
            this.dataContext.SaveChanges();
            return Ok(person);
        }

        [HttpPut]
        public IActionResult Put(Person person)
        {
            this.dataContext.Set<Person>().Update(person);
            this.dataContext.SaveChanges();
            return Ok(person);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var person = this.dataContext.Set<Person>().Find(id);
            this.dataContext.Set<Person>().Remove(person);
            this.dataContext.SaveChanges();
            return Ok(person);
        }
    }
}
