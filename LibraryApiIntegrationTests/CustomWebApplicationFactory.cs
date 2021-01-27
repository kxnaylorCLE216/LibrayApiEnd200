using LibraryApi;
using LibraryApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApiIntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // ask for the service that is implementing IGetServerStatus
                var serviceDescriptor = services.Single(services =>
                    services.ServiceType == typeof(IGetServerStatus));
                // remove it.
                services.Remove(serviceDescriptor);
                // replace it with a fake for testing.
                services.AddScoped<IGetServerStatus, FakeServerStatus>();
            });
        }
    }

    public class FakeServerStatus : IGetServerStatus
    {
        public Task<LibraryApi.Controllers.GetStatusResponse> GetCurrentStatus()
        {
            return Task.FromResult(new LibraryApi.Controllers.GetStatusResponse
            {
                Message = "Tacos are good.",
                LastChecked = new DateTime(1969, 4, 20, 23, 59, 00)
            });
        }
    }
}
