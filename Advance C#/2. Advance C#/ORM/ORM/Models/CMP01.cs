﻿using ServiceStack.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// Defines company details
    /// </summary>
    [Alias("CMP01")]
    public class CMP01
    {

        /// <summary>
        /// Declares id of company
        /// </summary>
        [AutoIncrement]
        public int P01F01 { get; set; }

        /// <summary>
        /// Declares name of company
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// Declares city of company
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Declares type of company
        /// </summary>
        public string P01F04 { get; set; }

        /// <summary>
        /// Declares number of employees in a company
        /// </summary>
        public int P01F05 { get; set; }


    }
}