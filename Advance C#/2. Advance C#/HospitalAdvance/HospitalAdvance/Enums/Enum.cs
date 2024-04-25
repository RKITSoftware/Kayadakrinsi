namespace HospitalAdvance.Enums
{
    /// <summary>
	/// Enum for operations
	/// </summary>
    public enum enmOperations
    {
        /// <summary>
        /// Insert
        /// </summary>
        I,

        /// <summary>
        /// Update
        /// </summary>
        U
    }

    /// <summary>
	/// Enum for working days of doctor
	/// </summary>
    public enum enmDaysOfWeek
    {
        /// <summary>
        /// Sunday
        /// </summary>
        Su = 1,

        /// <summary>
        /// Monday
        /// </summary>
        Mo = 2,

        /// <summary>
        /// Tuesday
        /// </summary>
        Tu = 4,

        /// <summary>
        /// Wednesday
        /// </summary>
        Wd = 8,

        /// <summary>
        /// Thursday
        /// </summary>
        Th = 16,

        /// <summary>
        /// Friday
        /// </summary>
        Fr = 32,

        /// <summary>
        /// Saturday
        /// </summary>
        Sa = 64,

        /// <summary>
        /// All days
        /// </summary>
        Al = Wk | We,

        /// <summary>
        /// Weekdays
        /// </summary>
        Wk = Mo | Tu | Wd | Th | Fr,

        /// <summary>
        /// Weekend
        /// </summary>
        We = Su | Sa
    }

    /// <summary>
	/// Defines role of helper
	/// </summary>
    public enum enmRole
    {
        /// <summary>
        /// Nurse
        /// </summary>
        NU,

        /// <summary>
        /// Clinical Assistant
        /// </summary>
        CA,

        /// <summary>
        /// Personal Service Assistant
        /// </summary>
        PSA,

        /// <summary>
        /// Ward Clerks
        /// </summary>
        WC,

        /// <summary>
        /// Volunteer
        /// </summary>
        VO
    }

    /// <summary>
    /// Enum for role of users
    /// </summary>
    public enum enmUserRole
    {
        /// <summary>
        /// Manager
        /// </summary>
        M,

        /// <summary>
        /// Doctor
        /// </summary>
        D,

        /// <summary>
        /// Helper
        /// </summary>
        H,

        /// <summary>
        /// Patient
        /// </summary>
        P
    }

    /// <summary>
    /// Enum for existance of user
    /// </summary>
    public enum enmIsActive
    {
        /// <summary>
        /// USer is active
        /// </summary>
        Y,

        /// <summary>
        /// User is not active
        /// </summary>
        N
    }
}