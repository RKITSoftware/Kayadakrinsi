using Microsoft.AspNetCore;
using RoutingAPI;

internal class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments passed to the application.</param>
    private static void Main(string[] args)
    {
        // Build the web host and run the application.
        BuildWebHost(args).Run();
    }

    /// <summary>
    /// Builds the web host for the application.
    /// </summary>
    /// <param name="args">The command-line arguments passed to the application.</param>
    /// <returns>The configured web host.</returns>
    public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>() // Specify the Startup class to use for configuring the application.
            .Build();
}



    //    var builder = WebApplication.CreateBuilder(args);

    //    // Add services to the container.

    //    builder.Services.AddControllers();
    //    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //    builder.Services.AddEndpointsApiExplorer();
    //    builder.Services.AddSwaggerGen();

    //    var app = builder.Build();

    //    // Configure the HTTP request pipeline.
    //    if (app.Environment.IsDevelopment())
    //    {
    //        app.UseSwagger();
    //        app.UseSwaggerUI();
    //    }

    //    app.UseHttpsRedirection();

    //    app.UseAuthorization();

    //    app.MapControllers();

    //    app.Run();