using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class GettingServerStatus : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public GettingServerStatus(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateDefaultClient();
        }

        // Do we get a 200
        [Fact]
        public async Task HasOkStatus()
        {
            var response = await _client.GetAsync("status");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Is the response encoded as application/json
        [Fact]
        public async Task IsEncodedAsJson()
        {
            var response = await _client.GetAsync("status");
            var contentType = response.Content.Headers.ContentType.MediaType;

            Assert.Equal("application/json", contentType);
        }

        // look at the response
        [Fact]
        public async Task HasProperEntity()
        {
            var response = await _client.GetAsync("/status");
            var content = await response.Content.ReadAsAsync<GetStatusResponse>();

            Assert.Equal("Tacos are good.", content.message);
            Assert.Equal(new DateTime(1969,4,20,23,59,00), content.lastChecked);
        }
    }


    public class GetStatusResponse
    {
        public string message { get; set; }
        public DateTime lastChecked { get; set; }
    }

}
