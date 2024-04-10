using BillingAPI.Enums;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.DTO
{
    public class DTOUSR01
    {
        [Required,Unique]
        [JsonProperty("R01F01")]
        public string R01101 { get; set; }

        [Required]
        [JsonProperty("R01F02")]
        public string R01102 { get; set; }

        [Required]
        [JsonProperty("R01F03")]
        public enmRoles R01103 { get; set; }
    }
}
