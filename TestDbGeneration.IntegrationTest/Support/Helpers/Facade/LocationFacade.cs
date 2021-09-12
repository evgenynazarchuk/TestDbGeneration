using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace TestDbGeneration.IntegrationTest.Support.Helpers.Facade
{
    public class LocationFacade : FacadeHelper
    {
        public LocationFacade(HttpClient client)
        : base(client) { }
    }
}
