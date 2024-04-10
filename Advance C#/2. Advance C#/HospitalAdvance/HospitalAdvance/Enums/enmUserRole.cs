using System;

namespace HospitalAdvance.Enums
{
    /// <summary>
	/// Enum for role of users
	/// </summary>
	[Flags]
    public enum enmUserRole
    {
        Manager = 1,
        Doctor = 2,
        Helper = 3,
        Patient = 4
    }

}