using ServiceStack.DataAnnotations;

namespace MusicCompany.Models
{
    [Alias("PRO01")]
    public class PRO01
    {
        /// <summary>
        /// Producer id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int O01F01 { get; set; }

        /// <summary>
        /// Producer name
        /// </summary>
        public string O01F02 { get; set; }

        /// <summary>
        /// Production company name
        /// </summary>
        public string O01F03 { get; set; }

        /// <summary>
        /// User id of producer
        /// </summary>
        [References(typeof(USR01))]
        public int O01F04 { get; set; }
    }
}