namespace FiltersAPI.Models
{
    /// <summary>
    /// Enum for telephone service type
    /// </summary>
    public enum enmServiceType
    {
        prepaid = 0,
        postpaid = 1
    }

    /// <summary>
    /// Telephone diary class
    /// </summary>
    public class TEL01
    {
        /// <summary>
        /// Serial number
        /// </summary>
        public int L01F01 { get; set; }

        /// <summary>
        /// Subscriber name
        /// </summary>
        public string L01F02 { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        public int L01F03 { get; set; }

        /// <summary>
        /// Subscriber telephone number
        /// </summary>
        public long L01F04 { get; set; } 

        /// <summary>
        /// Service type of subscriber
        /// </summary>
        public enmServiceType L01F05 { get; set; } = 0;
    }
}
