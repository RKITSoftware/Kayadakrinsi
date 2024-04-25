using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Charges  model for clientside
    /// </summary>
    public class DTOCRG01
    {
        /// <summary>
        /// Charge id
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [JsonProperty("G01101")]
        public int G01F01 { get; set; }

        /// <summary>
        /// Doctor id
        /// </summary>
        [Required, References(typeof(STF01))]
        [JsonProperty("G01102")]
        public int G01F02 { get; set; }

        /// <summary>
        /// Dieases id
        /// </summary>
        [Required, References(typeof(DIS01))]
        [JsonProperty("G01103")]
        public int G01F03 { get; set; }

        /// <summary>
        /// Charge amount per treatment
        /// </summary>
        [Required, DecimalLength(Precision = 10, Scale = 2)]
        [JsonProperty("G01104")]
        public double G01F04 { get; set; }

    }

}