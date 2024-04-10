namespace Logging
{
    /// <summary>
    /// Represents a startup class for configuring request processing pipeline and services of an application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures services required by an application
        /// </summary>
        /// <param name="services">Collection of servcies to be configured</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen();

            //EventLog.CreateEventSource("Logging", "Application");

            services.AddLogging(logging =>
            {
                logging.AddEventLog();
                //logging.AddEventLog(options =>
                //{
                //    options.LogName = "Application";
                //    options.SourceName = "Logging";
                //});
            });
            //if (!EventLog.SourceExists("Logging"))
            //{
            //    
            //}

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
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            });

            // Create a logger
            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("Logging information.");
            logger.LogCritical("Logging critical information.");
            logger.LogDebug("Logging debug information.");
            logger.LogError("Logging error information.");
            logger.LogTrace("Logging trace");
            logger.LogWarning("Logging warning.");
        }
    }
}


