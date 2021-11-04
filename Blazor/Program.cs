using Blazor.Helpers.LocalStorage;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddHttpClient(Globals.LOCAL_API, client =>
            {
                //client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // API hosted
                client.BaseAddress = new Uri("http://localhost:4020"); // CORS
            });

            builder.Services.AddScoped<HttpClient>(sp => {
                IHttpClientFactory factory = sp.GetRequiredService<IHttpClientFactory>();
                return factory.CreateClient(Globals.LOCAL_API);
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
