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

            builder.Services.AddScoped<IDataService, DataService>();


            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("el-GR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("el-GR");
            await builder.Build().RunAsync();
        }
    }
}
