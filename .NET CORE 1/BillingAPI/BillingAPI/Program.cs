using BillingAPI;
using Microsoft.AspNetCore;
using NLog.Web;

/// <summary>
/// Entry point of an application
/// </summary>
internal class Program
{
    /// <summary>
    /// Entry point of application
    /// </summary>
    /// <param name="args">Command line arguments</param>
    private static void Main(string[] args)
    {
        Builder(args).Run();
    }

    /// <summary>
    /// Builds web host for an application
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>Configured web host</returns>
    public static IWebHost Builder(string[] args) => 
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseNLog()
        .Build();
}