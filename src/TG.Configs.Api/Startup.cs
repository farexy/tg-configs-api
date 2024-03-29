using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TG.Configs.Api.Config;
using TG.Configs.Api.Db;
using TG.Configs.Api.ServiceClients;
using TG.Configs.Api.Services;
using TG.Core.App.Configuration;
using TG.Core.App.Configuration.Auth;
using TG.Core.App.InternalCalls;
using TG.Core.App.Middlewares;
using TG.Core.App.Swagger;
using TG.Core.Db.Postgres;
using TG.Core.Redis.Extensions;

namespace TG.Configs.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddTgJsonOptions()
                .AddInvalidModelStateHandling(); 
            services.AddHealthChecks();
            //.AddNpgSqlHealthCheck();
            //services.AddKubernetesTgApplicationInsights(Configuration);
            services.AddApiVersioning();

            services.AddPostgresDb<ApplicationDbContext>(Configuration, ServiceConst.ServiceName);
            
            services.AddCors(cors => cors.AddDefaultPolicy(p =>
            {
                p.AllowAnyHeader();
                p.AllowAnyMethod();
                p.AllowAnyOrigin();
            }));

            services.AddTgAuth(Configuration);
            services.AddAutoMapper<Startup>();
            services.AddMediatR(typeof(Startup));
                
            services.AddTgServices();

            services.ConfigureInternalCalls(Configuration);
            services.AddServiceClient<IAppsManagerClient>(Configuration.GetServiceInternalUrl(TgServices.Manager)); 

            services.AddTgRedis(Configuration);

            services.AddTgSwagger(opt =>
            {
                opt.ServiceName = ServiceConst.ServiceName;
                opt.ProjectName = ServiceConst.ProjectName;
                opt.AppVersion = "1";
            });

            services.AddHttpClient<ICallbacksClient, CallbacksClient>();

            services.AddSingleton<IConfigsCache, ConfigsCache>();
            services.AddScoped<IConfigsProvider, ConfigsProvider>();
            services.AddSingleton<ISecretsService>(sp => new SecretsService(new SecretClient(
                new Uri(Configuration.GetConnectionString("KeyVault")),
                new DefaultAzureCredential())));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseTgSwagger();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TracingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}