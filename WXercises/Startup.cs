using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using WXercises.Extensions;
using WXercises.Filters;
using WXercises.Proxies;
using WXercises.Services;

namespace WXercises
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("doc",
                        new Info
                        {
                            Title = typeof(Program).Assembly.GetName().Name,
                            Version = typeof(Program).Assembly.GetName().Version.ToString()
                        });
                })
                .AddMvc(options =>
                {
                    if (!_environment.IsDevelopment())
                    {
                        options.Filters.Add(typeof(RequireHttpsAttribute));
                    }
                    options.Filters.Add(typeof(ApiExceptionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(Startup)));

            // Application services
            services
                .AddSingleton<IApiResourceProxy, ApiResourceProxy>()
                .AddSingleton<IProductSorterFactory, ProductSorterFactory>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IProductsService, ProductsService>();
            
            services.AddHttpClientWithCircuitBreaker<IApiResourceProxy, ApiResourceProxy>(Constants.ProxyPrefix.ApiResourceProxy, _configuration);

            // JSON settings
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("doc/swagger.json", GetType().GetTypeInfo().Assembly.GetName().Name);
                });

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}
