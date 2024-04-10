using System;

namespace HospitalAdvance.Enums
{
    /// <summary>
	/// Defines role of helper
	/// </summary>
	[Flags]
    public enum enmRole
    {
        NURSE = 1,
        CLINICAL_ASSISTANT = 2,
        PATIENT_SERVICE_ASSISTANT = 3,
        WARD_CLERKS = 4,
        VOLUNTEER = 5
    }

}