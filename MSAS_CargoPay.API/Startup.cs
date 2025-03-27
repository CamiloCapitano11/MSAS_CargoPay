using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using MSAS_CargoPay.API.ExceptionMiddleware;
using MSAS_CargoPay.API.Extensions;
using MSAS_CargoPay.API.Helpers;
using MSAS_CargoPay.Application.CustomServices;
using MSAS_CargoPay.Application.Services;
using MSAS_CargoPay.Core.Interfaces;
using MSAS_CargoPay.Core.Resources;
using MSAS_CargoPay.Core.Services;
using MSAS_CargoPay.Infrastracture.Data;
using MSAS_CargoPay.Infrastracture.Logging;
using MSAS_CargoPay.Infrastracture.Repositories;

namespace MSAS_CargoPay.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddTransient<ICargoPayRepository>(provider => new CargoPayRepository(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICargoPayService, CargoPayService>();
            services.AddTransient<IServicesCacheHelper, ServicesCacheHelper>();


            services.AddSingleton<FileLogger>();
            services.AddSingleton<DatabaseLogger>();
            services.AddMemoryCache();
            services.AddDbContext<StoreContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.
                Parse(Configuration.GetConnectionString("Radis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddApplicationServices();
            services.AddHttpContextAccessor();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
            IdentityModelEventSource.ShowPII = true;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://login.microsoftonline.com/{Configuration["JwtSettings:Tenant"]}/";
                options.Audience = Configuration["JwtSettings:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = $"https://login.microsoftonline.com/{Configuration["JwtSettings:Tenant"]}/v2.0",
                    ValidAudience = Configuration["JwtSettings:Audience"]
                };


                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new
                            {
                                status = Messages.StatusError,
                                responseCode = HttpStatusCode.Unauthorized,
                                responseMessage = "Su token ha expirado. Por favor, inicie sesión nuevamente.",
                                data = ""
                            });
                            return context.Response.WriteAsync(result);
                        }
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        var generalErrorResult = JsonConvert.SerializeObject(new
                        {
                            status = Messages.StatusError,
                            responseCode = HttpStatusCode.Unauthorized,
                            responseMessage = "El token proporcionado es inválido.",
                            data = ""
                        });

                        return context.Response.WriteAsync(generalErrorResult);
                    }
                };
            });


            // Configura Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MSAS_CargoPay", Description = "Prueba técnica para HEYPRIMO!" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SchemaFilter<SwaggerExcludeFilter>();

                //Configura la seguridad con JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
                        new string[] {}
                    }
              });

            });
            //Cachet 
            services.AddMemoryCache();

            //Razor
            services.AddRazorPages();
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddle>();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSwaggerGen();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }

    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context?.Type == null)
                return;

            var excludedProperties = context.Type.GetProperties()
                .Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var propertyName = schema.Properties.Keys.SingleOrDefault(k => k.ToLower() == excludedProperty.Name.ToLower());
                if (propertyName != null)
                    schema.Properties.Remove(propertyName);
            }
        }
    }
}
