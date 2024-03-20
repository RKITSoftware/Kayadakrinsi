using Fundamentals;
using Microsoft.AspNetCore;

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
