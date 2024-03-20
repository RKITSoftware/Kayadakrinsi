using MiddleWareAPI.Middleware;

namespace MiddleWareAPI
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
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the request processing pipeline for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<CustomMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
