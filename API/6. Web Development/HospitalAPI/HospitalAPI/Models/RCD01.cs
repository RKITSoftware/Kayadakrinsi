using System;

namespace HospitalAPI.Models
{
    /// <summary>
    /// Declares data of record in hospital
    /// </summary>
    public class RCD01
    {
        #region Public Members

        /// <summary>
        /// Defines id of record
        /// </summary>
        public int D01F01 { get; set; }

        /// <summary>
        /// Defines name of patient
        /// </summary>
        public string D01F02 { get; set; }

        /// <summary>
        /// Defines name of doctor
        /// </summary>
        public string D01F03 { get; set; }

        /// <summary>
        /// Defines name of dieases
        /// </summary>
        public string D01F04 { get; set; }

        /// <summary>
        /// Defines admit date
        /// </summary>
        public DateTime D01F05 { get; set; }

        /// <summary>
        /// Defines discharge date
        /// </summary>
        public DateTime D01F06 { get; set; }

        /// <summary>
        /// Defines charge
        /// </summary>
        public double D01F07 { get; set; }

        #endregion
    }
}