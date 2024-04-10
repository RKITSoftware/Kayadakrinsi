using System.Diagnostics.Tracing;

namespace Logging
{
    /// <summary>
    /// Represents class of Event source demo purpose
    /// </summary>
    [EventSource(Name = "Demo")]
    class DemoEventSource : EventSource
    {
        /// <summary>
        /// Instance of DemoEventSource class
        /// </summary>
        public static DemoEventSource Log { get; } = new DemoEventSource();

        /// <summary>
        /// Writes into EventSource log when App is started
        /// </summary>
        /// <param name="message"></param>
        /// <param name="favoriteNumber"></param>
        [Event(1)]
        public void AppStarted(string message, int favoriteNumber) => WriteEvent(1, message, favoriteNumber);
    }
}
