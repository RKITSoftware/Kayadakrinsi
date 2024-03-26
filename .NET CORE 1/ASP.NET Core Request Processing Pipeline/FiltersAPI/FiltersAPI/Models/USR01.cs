namespace FiltersAPI.Models
{
    /// <summary>
    /// Enum for user role
    /// </summary>
    public enum enmUserRole
    {
        Admin = 0,
        Guest = 1
    }

    /// <summary>
    /// User class
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User id
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public enmUserRole R01F04 { get; set; } = 0;
    }
}
