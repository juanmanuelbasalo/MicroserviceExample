using Api;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActivitiesTest.Integration.Controllers
{
    public class HomeControllerTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public HomeControllerTests()
        {
            server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Theory]
        [InlineData("home/")]
        public async Task Home_controller_get_should_return_string_content(string resource)
        {
            client.BaseAddress = new Uri("https://localhost:5001/api/");
            var response = await client.GetAsync(resource);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Testing Microservices.");
        }
    }
}
