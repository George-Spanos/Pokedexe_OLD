using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokedexChat.Data;
namespace PokedexChat {
    public class Program {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options => { builder.Configuration.Bind("Authentication:Google", options.ProviderOptions); });

            // Blazor WebAssembly apps don't currently have a concept of DI scopes.
            // Scoped-registered services behave like Singleton services.
            
            builder.Services.AddSingleton<IDataService, DataService>();
            
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("el-GR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("el-GR");
            
            var host = builder.Build();
            var dataService = host.Services.GetRequiredService<IDataService>();
            await dataService.InitializeAsync();
            await host.RunAsync();
        }
    }
}
