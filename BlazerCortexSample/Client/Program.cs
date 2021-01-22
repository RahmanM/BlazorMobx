using BlazerCortexSample.Client.Store;
using Blazored.LocalStorage;
using Cortex.Net;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazerCortexSample.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // add local storage support.
            builder.Services.AddBlazoredLocalStorage();

            // Add the Shared state to the DI container.
            builder.Services.AddSingleton(x => SharedState.GlobalState);

            // Add a singleton ViewStore to the DI container.
            builder.Services.AddSingleton<ViewStore>();

            // Add a singleton TodoStore to the DI container.
            builder.Services.AddSingleton<TodoStore>();

            await builder.Build().RunAsync();
        }
    }
}
