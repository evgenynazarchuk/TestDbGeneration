using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using TestDbGeneration.Models;

namespace TestDbGeneration.IntegrationTest.Support.Helpers.Facade
{
    public class PersonFacade : FacadeHelper
    {
        public PersonFacade(HttpClient client)
            : base(client) { }

        public ActionResult<List<Person>> Get()
        {
            return this.Get<List<Person>>("person");
        }
    }
}
