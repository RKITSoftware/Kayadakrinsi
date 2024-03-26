using FiltersAPI.Filters;
using Microsoft.OpenApi.Models;

namespace FiltersAPI
{
    /// <summary>
    /// Represents the startup class responsible for configuring the application's services and request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });

            services.AddMemoryCache();
            services.AddSingleton<ResourceFilter>();
            services.AddSingleton<ActionFilter>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Filters", Version = "v1" });
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
            });
        }

        /// <summary>
        /// Configures the request processing pipeline for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
