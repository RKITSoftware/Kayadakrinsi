using BillingAPI.Enums;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Represents model for user
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        public enmRoles R01F04 { get; set; }

    }
}
