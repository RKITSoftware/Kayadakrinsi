using BillingAPI.Enums;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Represents model class for company
    /// </summary>
    public class CMP01
    {
        /// <summary>
        /// Id of the company
        /// </summary>
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// GST number of the company
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Address of the company
        /// </summary>
        public string P01F04 { get; set; }

        /// <summary>
        /// Name of state in which company is located
        /// </summary>
        public enmStateUT P01F05{ get; set; } 
    }
}
