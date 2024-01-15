using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        #region Constructors

        public RCD01(int id,string patient_name,string doctor_name,string dieases_name,DateTime admit,DateTime discharge,double charge)
        {
                D01F01 = id;
                D01F02 = patient_name;
                D01F03 = doctor_name;
                D01F04 = dieases_name;
                D01F05 = admit;
                D01F06 = discharge; 
                D01F07 = charge;
        }

        #endregion

        #region Public Methods

        public static List<RCD01> GetRecords()
        {
            var records = new List<RCD01> { 
                new RCD01(1,"NILESH RAY","SWETA SHARMA","CANCER",new DateTime(2023,10,10),new DateTime(2023,12,08),1345.45),
                new RCD01(2,"ROHIT SHAH","KIRIT KANANI","TYPHOID",new DateTime(2023,10,12),new DateTime(2023,11,11),1245.33),
                new RCD01(3,"SAKINA MEMAN","RAJESH SINHA","CHOLERA",new DateTime(2023,10,20),new DateTime(2023,11,20),4561.12),
                new RCD01(4,"HIMESH HIMMATVALA","ARTI ROY","CHIKENPOX",new DateTime(2023,11,02),new DateTime(2023,11,25),451.10),
                new RCD01(5,"RAHUL SHETTY","GUAVRAV PANDAYA","DIABETES",new DateTime(2023,11,17),new DateTime(2023,11,30),1365.29),
                new RCD01(6,"SUMIT SINGH","ISHA GUPTA","DEPRESSION",new DateTime(2023,11,19),new DateTime(2023,12,01),1512.61),
                new RCD01(7,"NISHA SHARMA","PRAFUL ARORA","PNEUMONIA",new DateTime(2023,11,21),new DateTime(2023,12,11),2152.21),
                new RCD01(8,"VIMAL VERMA","SHAILESH SHAH","HEART DIEASES",new DateTime(2023,12,10),new DateTime(2023,12,25),6321.2),
                new RCD01(9,"JAY PATEL","BIPIN PAL","ALZER",new DateTime(2023,12,25),new DateTime(2023,12,30),2031.11),
                new RCD01(10,"KIRTAN KOHLI","JIGNESH ANSARI","DRY EYE",new DateTime(2023,12,31),new DateTime(2024,01,11),2024.24)


            };
            return  records;
        }
        #endregion
    }
}