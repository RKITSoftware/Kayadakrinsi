using DependencyInjection.Interfaces;
using DependencyInjection.Services;
using ServiceLifetime = DependencyInjection.Services.ServiceLifetime;

namespace DependencyInjection
{
    /// <summary>
    /// Configures request processing pipeline ans services which are required for an application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures services used by an application 
        /// </summary>
        /// <param name="services">Collection of services to configure</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<IOperations, OperationsAPL01>();

            services.AddSingleton<ISingletone,ServiceLifetime>();

            services.AddTransient<ITransient, ServiceLifetime>();

            services.AddScoped<IScopped, ServiceLifetime>();

        }

        /// <summary>
        /// Configures request processing pipeline for an application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
