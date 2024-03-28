using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DependencyInjection.Models
{
    /// <summary>
    /// Enum for shift of exercise program
    /// </summary>
    public enum enmShift
    {
        Morning = 0,
        Evening = 1
    }

    /// <summary>
    /// Enum for purpose of exercise program
    /// </summary>
    public enum enmPurpose
    {
        WeightLoss = 0,
        Strength = 1,
        Exercise = 2
    }

    /// <summary>
    /// Application for gym membership class
    /// </summary>
    public class APL01
    {
        /// <summary>
        /// Application ID
        /// </summary>
        public int L01F01 { get; set; }

        /// <summary>
        /// Name of applicant
        /// </summary>
        public string L01F02 { get; set; }

        /// <summary>
        /// BMI of applicant
        /// </summary>
        public int L01F03 { get; set; }

        /// <summary>
        /// Hours can be given to exercise program per day
        /// </summary>
        public double L01F04 { get; set; } = 1;

        /// <summary>
        /// Shift of exercise program
        /// </summary>
        public enmShift L01F05 { get; set; } = enmShift.Morning;

        /// <summary>
        /// Purpose of exercise program
        /// </summary>
        public enmPurpose L01F06 { get; set; } = enmPurpose.Exercise;

        /// <summary>
        /// Fee of membership
        /// </summary>
        public double L01F07 { get; set; } = 1000;

        /// <summary>
        /// Joining date of applicant
        /// </summary>
        public DateTime L01F08 { get; set; } = DateTime.Now;

    }
}
