using BillingAPI.Filters;
using BillingAPI.Interfaces;
using BillingAPI.Models.POCO;
using BillingAPI.Repositaries;
using Microsoft.OpenApi.Models;
using ServiceStack;

namespace BillingAPI
{
    /// <summary>
    /// Represents startup class which is used to configure application services and request processing pipeline
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Configures services required for an application
        /// </summary>
        /// <param name="services">Collection of services to be configured</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Billing", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });

                //// Use Newtonsoft.Json for Swagger JSON serialization
                //c.CustomSchemaIds(type => type.FullName);
                ////c.UseNewtonsoftJson();
                //c.ToJson();
            });

            services.AddControllers(config =>
            {
                // Applies filter globally
                config.Filters.Add(new AuthenticationFilter());
            });

            services.AddLogging(logging =>
            {
                logging.AddEventLog();
            });

            // Registers interface service and their implementation
            services.AddTransient<ICRUDService<PRO01>, CRUDImplementation<PRO01>>();
            services.AddTransient<ICRUDService<CMP01>, CRUDImplementation<CMP01>>();
            services.AddTransient<ICRUDService<BIL01>, CRUDImplementation<BIL01>>();
            services.AddTransient<IBIL01Service, DbBIL01Context>();
            // services.AddSingleton<ResourceFilter>
            // services.AddTransient<ICRUD<USR01>, CRUDImplementation<USR01>>();
            // services.AddTransient<IActionFilter, ActionExecutedFilter>();

        }

        /// <summary>
        /// Configures request processing pipeline of an application
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="environment">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                }
            );
        }
    }
}
