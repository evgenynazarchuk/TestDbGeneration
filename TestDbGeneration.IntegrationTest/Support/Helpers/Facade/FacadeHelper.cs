using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net;

namespace TestDbGeneration.IntegrationTest.Support.Helpers.Facade
{
    public class FacadeHelper
    {
        private static JsonSerializerOptions JsonOptionCamelCase = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient client;

        public FacadeHelper(HttpClient client)
        {
            this.client = client;
        }

        public ActionResult<TType> Get<TType>(string url = null)
        {
            var response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApplicationException(FacadeErrorMessage.InternalServerError);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ApplicationException(FacadeErrorMessage.NotFound);
            }

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonSerializer.Deserialize<TType>(content, JsonOptionCamelCase);
            return new ActionResult<TType>(result);
        }
        public ActionResult<TType> Post<TType>(string url, TType obj)
        {
            var requestPayload = JsonSerializer.Serialize(obj);
            var response = client
                .PostAsync(url, new StringContent(requestPayload, Encoding.UTF8, "application/json"))
                .GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApplicationException(FacadeErrorMessage.InternalServerError);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ApplicationException(FacadeErrorMessage.NotFound);
            }

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorList = JsonSerializer.Deserialize<List<string>>(content, JsonOptionCamelCase);
                return new ActionResult<TType>().AddErrors(errorList);
            }

            var result = JsonSerializer.Deserialize<TType>(content, JsonOptionCamelCase);
            return new ActionResult<TType>(result);
        }
        public ActionResult<bool> Put<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var response = client
                .PutAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"))
                .GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApplicationException(FacadeErrorMessage.InternalServerError);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ApplicationException(FacadeErrorMessage.NotFound);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var errorList = JsonSerializer.Deserialize<List<string>>(content, JsonOptionCamelCase);
                return new ActionResult<bool>().AddErrors(errorList);
            }

            return new ActionResult<bool>(true);
        }
        public ActionResult<bool> Delete(string url)
        {
            var response = client.DeleteAsync(url).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApplicationException(FacadeErrorMessage.InternalServerError);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ApplicationException(FacadeErrorMessage.NotFound);
            }

            return new ActionResult<bool>(true);
        }
    }
}
