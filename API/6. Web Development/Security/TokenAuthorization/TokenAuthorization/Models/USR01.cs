namespace TokenAuthorization.Models
{
    /// <summary>
    /// Declares users
    /// </summary>
    public class USR01
    {
        #region Public Members

        /// <summary>
        /// Declares id of user
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Declares user name of user
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Declares password of user
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Declares role of user
        /// </summary>
        public string R01F04 { get; set; }

        #endregion

    }
}

