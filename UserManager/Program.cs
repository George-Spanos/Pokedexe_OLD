using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
namespace UserManager {
    public class Program {
        public static int Main(string[] args)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=pokedexestorage;AccountKey=9nx8W58vEFpMywuxiPP63VDSC2Y8NEGmo/8gKL/S1uz/d8kl8ZHQdQ6IdtaAAnvrM2eWfX1Jwnt4RTb69eBFpQ==;EndpointSuffix=core.windows.net";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.AzureBlobStorage(connectionString, LogEventLevel.Information, null, "{yyyy}/{MM}/{dd}/log.txt")
                .CreateBootstrapLogger();
            try{
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex){
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally{
                Log.CloseAndFlush();
            }
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
