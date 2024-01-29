using ServiceStack.DataAnnotations;

namespace MusicCompany.Models
{
    [Alias("USR01")]
    public class USR01
    {
        /// <summary>
        /// User id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        public string R01F04 { get; set; }
    }
}