using System;

namespace HospitalAdvance.Enums
{
    /// <summary>
	/// Enum for working days of doctor
	/// </summary>
	[Flags]
    public enum enmDaysOfWeek
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,

        All = Weekdays | Weekend,
        Weekdays = Monday | Tuesday | Wednesday | Thursday | Friday,
        Weekend = Sunday | Saturday
    }
}