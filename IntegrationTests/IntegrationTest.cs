using kinder_app;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    /*
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.Remove(typeof(DataContext));
                    });

                });
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("bearer", await GetJwtAsync());
        }
        private async Task<String> GetJwtAsync()
        {
            
        }
    }
    */
}
