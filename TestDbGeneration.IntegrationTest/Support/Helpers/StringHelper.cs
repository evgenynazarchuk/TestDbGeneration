using System;
using System.Linq;

namespace TestDbGeneration.IntegrationTest.Support.Helpers
{
    public static class StringHelper
    {
        public static string GenerateString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();

            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }
    }
}
