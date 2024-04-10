using BillingAPI.Enums;
using Newtonsoft.Json;
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
        [AutoIncrement, PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        [JsonProperty("R01101")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [JsonProperty("R01102")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        [JsonProperty("R01103")]
        public enmRoles R01F04 { get; set; }

    }
}
