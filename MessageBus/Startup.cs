using System;
using System.Linq;
using MessageBus.Common;
using MessageBus.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.AspNetCore.Extensions;
using MQTTnet.Server;
namespace MessageBus {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private const string ALLOW_ALL_POLICY = "AllowAll";
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMqttControllers();

            services
                .AddHostedMqttServer(mqttServer => mqttServer.WithoutDefaultEndpoint())
                .AddHostedMqttServerWithServices(mqttServer => {
                    mqttServer.WithoutDefaultEndpoint();
                    mqttServer.WithAttributeRouting();
                })
                .AddMqttConnectionHandler()
                .AddConnections();
            services.AddCors(o => o.AddPolicy(ALLOW_ALL_POLICY,
            builder => {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddScoped<IMessageStoreService, AzureTableStorageMessageService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors(ALLOW_ALL_POLICY);
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapConnectionHandler<MqttConnectionHandler>("/mqtt");
            });
            app.UseMqttServer(server => server.StartedHandler = new MqttServerStartedHandlerDelegate(_ => {
                Console.WriteLine("Server started");
            }));

        }
    }
}
