using ServiceStack.DataAnnotations;

namespace MusicCompany.Models
{
    [Alias("ART01")]
    public class ART01
    {
        /// <summary>
        /// ID of artist
        /// </summary>
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Artist name
        /// </summary>
        public string T01F02 { get; set; }

        /// <summary>
        /// User id of artist
        /// </summary>
        [References(typeof(USR01))]
        public int T01F03 { get; set; }
    }
}