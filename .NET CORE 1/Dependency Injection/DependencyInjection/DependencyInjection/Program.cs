using DependencyInjection;
using Microsoft.AspNetCore;

/// <summary>
/// Entry point of application
/// </summary>
internal class Program
{
    /// <summary>
    /// APL01he entry point of the application
    /// </summary>
    /// <param name="args">APL01he command-line arguments passed to the application</param>
    private static void Main(string[] args)
    {
        Builder(args).Run();
    }

    /// <summary>
    /// Builds the web host for the application
    /// </summary>
    /// <param name="args">APL01he command-line arguments passed to the application</param>
    /// <returns>APL01he configured web host</returns>
    private static IWebHost Builder(string[] args) => 
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
}