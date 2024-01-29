using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace MusicCompany.Models
{
    [Alias("ALB01")]
    public class ALB01
    {
        /// <summary>
        /// Album id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int B01F01 { get; set; }

        /// <summary>
        /// Album name
        /// </summary>
        public string B01F02 { get; set; }

        /// <summary>
        /// Total number of song
        /// </summary>
        public int B01F03 { get; set; }

        /// <summary>
        /// Producer id
        /// </summary>
        [References(typeof(PRO01))]
        public int B01F04 { get; set; }

        /// <summary>
        /// Artist id
        /// </summary>
        [References(typeof(ART01))]
        public int B01F05 { get; set;}
    }
}