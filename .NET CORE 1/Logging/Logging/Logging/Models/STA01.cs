namespace Logging.Models
{
    /// <summary>
    /// Represents model for stationary class
    /// </summary>
    public class STA01
    {
        /// <summary>
        /// Id of stationary item
        /// </summary>
        public int A01F01 { get; set; }

        /// <summary>
        /// Name of stationary item
        /// </summary>
        public string A01F02 { get; set; }
        
        /// <summary>
        /// Purchasing price of stationary item
        /// </summary>
        public double A01F03 { get; set; }
        
        /// <summary>
        /// Selling price of stationary item
        /// </summary>
        public double A01F04 { get; set; }

    }
}
