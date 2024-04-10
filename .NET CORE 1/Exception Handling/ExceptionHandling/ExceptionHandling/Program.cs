using ExceptionHandling;
using Microsoft.AspNetCore;

/// <summary>
/// Entry point of an application
/// </summary>
internal class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments passed to the application.</param>
    private static void Main(string[] args)
    {
       Builder(args).Run();
    }

    /// <summary>
    /// Builds the web host for the application.
    /// </summary>
    /// <param name="args">The command-line arguments passed to the application.</param>
    /// <returns>The configured web host.</returns>
    public static IWebHost Builder(string[] args) => 
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
}