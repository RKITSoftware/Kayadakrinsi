namespace Sealed_class
{
    /// <summary>
    /// User class two tries to inherit from sealed class
    /// </summary>
    public class USR02 : PersonalDetails
    {
        /// <summary>
        /// Id of user
        /// </summary>
        public int R02F01 { get; set; }

        /// <summary>
        /// username of user
        /// </summary>
        public string R02F02 { get; set; }

        /// <summary>
        /// password of user
        /// </summary>
        public string R02F03 { get; set; }
    }
}
