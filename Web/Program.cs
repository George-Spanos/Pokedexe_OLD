using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
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

            ConfigureServices(builder);
            await StartUpOperations(builder);
        }

        private static void ConfigureServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options => {
                builder.Configuration.Bind("Authentication:Google", options.ProviderOptions);
                
            });

            // Blazor WebAssembly apps don't currently have a concept of DI scopes.
            // Scoped-registered services behave like Singleton services.

            builder.Services.AddScoped<IMessageDataService, MessageDataService>();
            builder.Services.AddScoped<IUserDataService, UserDataService>();
            builder.Services.AddScoped<IDataService, DataService>();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("el-GR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("el-GR");
        }
        private static async Task StartUpOperations(WebAssemblyHostBuilder builder)
        {
            var host = builder.Build();
            // var dataService = host.Services.GetRequiredService<IDataService>();
            // await dataService.InitializeAsync();
            await host.RunAsync();
        }

    }
}
