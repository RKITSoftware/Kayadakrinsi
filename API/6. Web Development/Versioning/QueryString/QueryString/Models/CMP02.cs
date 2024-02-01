namespace QueryString.Models
{

    /// <summary>
    /// Defines company details
    /// </summary>
    public class CMP02
    {
        #region Public Members

        /// <summary>
        /// Declares id of company
        /// </summary>
        public int P02F01 { get; set; }

        /// <summary>
        /// Declares name of company
        /// </summary>
        public string P02F02 { get; set; }

        /// <summary>
        /// Declares country of company
        /// </summary>
        public string P02F03 { get; set; }

        /// <summary>
        /// Declares type of company
        /// </summary>
        public string P02F04 { get; set; }

        /// <summary>
        /// Declares number of employees in a company
        /// </summary>
        public int P02F05 { get; set; }

        #endregion

    }
}