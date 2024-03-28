namespace ActionMethods.Models
{
    /// <summary>
    /// Computer class
    /// </summary>
    public class COM01
    {
        /// <summary>
        /// Computer's product id
        /// </summary>
        public Guid M01F01 { get; set; }

        /// <summary>
        /// Computer model
        /// </summary>
        public string M01F02 { get; set; }

        /// <summary>
        /// Computer's production company
        /// </summary>
        public string M01F03 { get; set; }

        /// <summary>
        /// Processor specification
        /// </summary>
        public string M01F04 { get; set; }

        /// <summary>
        /// RAM capacity in GHZ
        /// </summary>
        public int M01F05 { get; set; } = 32;

        /// <summary>
        /// Price of computer
        /// </summary>
        public decimal M01F06 { get; set; } = 60000;
    }
}
