using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManager.Common;
using UserManager.Services;
namespace UserManager {
    public class Startup {
        private const string ALLOW_ALL_POLICY = "AllowAll";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddCors(o => o.AddPolicy(ALLOW_ALL_POLICY,
            builder => {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddScoped<IUserStoreService<AzureTableUser>, AzureTableStorageUserService>();
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
            });
        }
    }
}
