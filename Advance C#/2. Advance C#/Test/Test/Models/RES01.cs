using System.Data;

namespace Test.Models
{
    /// <summary>
    /// Represents result view model
    /// </summary>
    public class RES01
    {
        /// <summary>
        /// Flag indicating presence of errors
        /// </summary>
        public bool isError { get; set; } = false;

        /// <summary>
        /// Response message if any
        /// </summary>
        public string message { get; set; } = "";

        /// <summary>
        /// Response
        /// </summary>
        public DataTable response { get; set; } = new DataTable();
    }
}