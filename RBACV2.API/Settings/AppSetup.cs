using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using RBACV2.API.ExceptionFilters;
using RBACV2.API.Middlewares;
using RBACV2.Application;
using RBACV2.Application.Common.Settings;
using RBACV2.Infrastructure;
using RBACV2.Infrastructure.Services.PermissionsHandler;
using System.Text.Json.Serialization;

namespace RBACV2.API.Settings
{
    public class AppSetup
    {
        public AppSetup(ConfigurationManager configuration)
        {
            Configuration = configuration;
        }
        public ConfigurationManager Configuration { get; set; }
        public void Configure(IWebHostEnvironment env)
        {
            Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                optional: false, reloadOnChange: true);
        }
        public void RegisterServices(IServiceCollection services)
        {
            services.Configure<AzureAdClientSettings>(Configuration.GetSection("AzureAdClientSettings"));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddHttpContextAccessor();
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RBACV2.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        builder.SetIsOriginAllowed(x => true);
                    });
            });
        }
        public void SetupMiddlewares(WebApplication app)
        {
            app.UseMiddleware<UnauthorizedMiddleware>();
            app.UseCors("DevPolicy");

        }
    }
}
