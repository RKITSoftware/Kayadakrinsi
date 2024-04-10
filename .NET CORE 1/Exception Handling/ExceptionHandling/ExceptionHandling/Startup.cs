using Microsoft.AspNetCore.Diagnostics;

namespace ExceptionHandling
{
    /// <summary>
    /// Represents the startup class responsible for configuring request processing pipeline and services of an application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configure services used by an application
        /// </summary>
        /// <param name="services">Collection of services to be configure</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures request processing pipeline of an application
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="environment">Hosting environment</param>
        public void Configure(IApplicationBuilder app,IWebHostEnvironment environment)
        {
            if(environment.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 5
                };

                //Passing DeveloperExceptionPageOptions Instance to UseDeveloperExceptionPage Middleware Component
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
                
                //app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    // Custom logic for handling the exception
                    // ...
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<html><body>\r\n");
                    await context.Response.WriteAsync("Custom Error Page<br><br>\r\n");
                    // Display custom error details
                    await context.Response.WriteAsync($"<strong>Error:</strong> {exception.Message}<br>\r\n");
                    await context.Response.WriteAsync("</body></html>\r\n");
                }));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}
