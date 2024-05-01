using BillingAPI.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BillingAPI.Models.DTO
{
    /// <summary>
    /// User model for clientside
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        [Required(ErrorMessage = "User name is required")]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        [Required(ErrorMessage = "User role is required")]
        [JsonProperty("R01104")]
        public enmRoles R01F04 { get; set; }
    }
}
