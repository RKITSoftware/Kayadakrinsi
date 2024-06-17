using Logging;
using Microsoft.AspNetCore;
using NLog.Web;

/// <summary>
/// Entry point of an application
/// </summary>
internal class Program
{

    /// <summary>
    /// Entry point for an application
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        NLog.Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        
        try
        {
            DemoEventSource.Log.AppStarted("Hello World!", 12);
            Builder(args).Run();
        }
        catch (Exception ex)
        {
            logger.Fatal(ex);
        }
        
    }

    /// <summary>
    /// Builds web host for an application
    /// </summary>
    /// <param name="args">Command line arguments if any</param>
    /// <returns>Configured web host</returns>
    public static IWebHost Builder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureLogging(logging =>
        {
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        })
        .UseNLog()
        .Build();

}