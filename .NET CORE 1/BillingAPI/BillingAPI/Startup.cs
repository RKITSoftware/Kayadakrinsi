using BillingAPI.Extentions;
using BillingAPI.Filters;
using BillingAPI.Interfaces;
using BillingAPI.Middlewares;
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

                // Bearer Authentication
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                // Security Requirements
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            services.AddControllers(config =>
            {
                // Applies filter globally
                config.Filters.Add(typeof(Filters.CustomExceptionFilter));
            }).AddNewtonsoftJson();

            services.AddLogging(logging =>
            {
                logging.AddEventLog();
            });

            // Registers interface service and their implementation
            services.AddCustomServices();

            services.AddSwaggerGenNewtonsoftSupport();
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

            app.UseMalwareDetectionMiddleware();

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
