using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URI.Models
{
    /// <summary>
    /// Defines company details
    /// </summary>
    public class CMP01
    {
        #region Public Members

        /// <summary>
        /// Declares id of company
        /// </summary>
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

        #endregion
    }
}