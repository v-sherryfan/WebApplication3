using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using WebApplication3;

using Xunit;

namespace TestProject2
{
    public class PrimeWeb_DefaultRequestShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public PrimeWeb_DefaultRequestShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Hello World!",
                responseString);
        }
    }
}
