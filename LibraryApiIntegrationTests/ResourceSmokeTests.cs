using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class ResourceSmokeTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public ResourceSmokeTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateDefaultClient();

        }

        [Theory]
        [InlineData("status")]
        [InlineData("employees")]
        [InlineData("sayhi/bob")]
        public async Task ResourcesAreAliveAndKickingAsync(string resource)
        {
            var response = await _client.GetAsync(resource);

            Assert.True(response.IsSuccessStatusCode); // 200-299
        }

        
    }
}
