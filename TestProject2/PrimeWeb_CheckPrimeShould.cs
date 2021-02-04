﻿using Microsoft.AspNetCore.Hosting;
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
    public class PrimeWeb_CheckPrimeShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public PrimeWeb_CheckPrimeShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        private async Task<string> GetCheckPrimeResponseString(
            string querystring = "")
        {
            string request = "/checkprime";
            if (!String.IsNullOrEmpty(querystring))
            {
                request += "?" + querystring;
            }
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task ReturnInstructionsGivenEmptyQueryString()
        {
            // Act
            var responseString = await GetCheckPrimeResponseString();

            // Assert
            Assert.Equal("Pass in a number to check in the form /checkprime?5",
                responseString);
        }
        [Fact]
        public async Task ReturnPrimeGiven5()
        {
            // Act
            var responseString = await GetCheckPrimeResponseString("5");

            // Assert
            Assert.Equal("5 is prime!",
                responseString);
        }

        [Fact]
        public async Task ReturnNotPrimeGiven6()
        {
            // Act
            var responseString = await GetCheckPrimeResponseString("6");

            // Assert
            Assert.Equal("6 is NOT prime!",
                responseString);
        }
    }

}
