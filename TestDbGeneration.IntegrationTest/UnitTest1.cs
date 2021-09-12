using System;
using System.Threading.Tasks;
using Xunit;
using TestDbGeneration.IntegrationTest.Support;

namespace TestDbGeneration.IntegrationTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            using var env = new TestEnvironment();
            await env.HttpClient.GetAsync("person");
        }
    }
}
