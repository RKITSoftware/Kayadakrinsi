using System;
using System.Collections.Generic;
using HospitalAPI.Models;

namespace HospitalAPI.BusinesLogic
{
    /// <summary>
    /// Contains business logic for controllers
    /// </summary>
    public class BLRecord
    {
        #region Public Members 

        /// <summary>
        /// List of records
        /// </summary>
        public static List<RCD01> lstRCD01 = new List<RCD01>
        {
                new RCD01{D01F01=0,D01F02="KIRTAN KOHLI",D01F03="JIGNESH ANSARI",D01F04="DRY EYE",D01F05= new DateTime(2023,12,31),D01F06=new DateTime(2024,01,11),D01F07=2024.24 },
                new RCD01{D01F01=1,D01F02="NILESH RAY",D01F03="SWETA SHARMA",D01F04="CANCER",D01F05=new DateTime(2023,10,10),D01F06=new DateTime(2023,12,08),D01F07=1345.45 },
                new RCD01{D01F01=2,D01F02="ROHIT SHAH",D01F03="KIRIT KANANI",D01F04="TYPHOID",D01F05=new DateTime(2023,10,12),D01F06=new DateTime(2023,11,11),D01F07=1245.33 },
                new RCD01{D01F01=3,D01F02="SAKINA MEMAN",D01F03="RAJESH SINHA",D01F04="CHOLERA",D01F05=new DateTime(2023,10,20),D01F06=new DateTime(2023,11,20),D01F07=4561.12 },
                new RCD01{D01F01=4,D01F02="HIMESH HIMMATVALA",D01F03="ARTI ROY",D01F04="CHIKENPOX",D01F05=new DateTime(2023,11,02),D01F06=new DateTime(2023,11,25),D01F07=451.10 },
                new RCD01{D01F01=5,D01F02="RAHUL SHETTY",D01F03="GUAVRAV PANDAYA",D01F04="DIABETES",D01F05=new DateTime(2023,11,17),D01F06=new DateTime(2023,11,30),D01F07=1365.29 },
                new RCD01{D01F01=6,D01F02="SUMIT SINGH",D01F03="ISHA GUPTA",D01F04="DEPRESSION",D01F05=new DateTime(2023,11,19),D01F06=new DateTime(2023,12,01),D01F07=1512.61 },
                new RCD01{D01F01=7,D01F02="NISHA SHARMA",D01F03="PRAFUL ARORA",D01F04="PNEUMONIA",D01F05=new DateTime(2023,11,21),D01F06=new DateTime(2023,12,11),D01F07=2152.21 },
                new RCD01{D01F01=8,D01F02="VIMAL VERMA",D01F03="SHAILESH SHAH",D01F04="HEART DIEASES",D01F05=new DateTime(2023,12,10),D01F06=new DateTime(2023,12,25),D01F07=6321.2 },
                new RCD01{D01F01=9,D01F02="JAY PATEL",D01F03="BIPIN PAL",D01F04="ALZER",D01F05=new DateTime(2023,12,25),D01F06=new DateTime(2023,12,30),D01F07=2031.11 }
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles get request for records whoose id is less than four
        /// </summary>
        /// <returns>List of records</returns>
        public List<RCD01> GetSomeRecords()
        {
            return lstRCD01.FindAll(c => c.D01F01 < 4);
        }

        /// <summary>
        /// Handles get request for records whoose id is less than seven 
        /// </summary>
        /// <returns>List of records</returns>
        public List<RCD01> GetMoreRecords()
        {
            return lstRCD01.FindAll(c => c.D01F01 < 7);
        }

        /// <summary>
        /// Adds record to list
        /// </summary>
        /// <param name="objRCD01">Object to be add</param>
        /// <returns>List of records</returns>
        public List<RCD01> AddRecord(RCD01 objRCD01)
        {
            var record = lstRCD01.Find(r => r.D01F01 == objRCD01.D01F01);
            if (record == null)
            {
                lstRCD01.Add(objRCD01);
            }
            return lstRCD01;
        }

        /// <summary>
        /// Edits record of list
        /// </summary>
        /// <param name="objRCD01">record to be edit</param>
        /// <returns>List of records</returns>
        public List<RCD01> EditRecord(RCD01 objRCD01)
        {
            var record = lstRCD01.Find(r => r.D01F01 == objRCD01.D01F01);
            if (record != null)
            {
                lstRCD01[objRCD01.D01F01] = objRCD01;
            }
            return lstRCD01;
        }

        /// <summary>
        /// Removes record from list
        /// </summary>
        /// <param name="id">id of record to be delete</param>
        /// <returns>List of records</returns>
        public List<RCD01> DeleteRecord(int id)
        {
            var record = lstRCD01.Find(r => r.D01F01 == id);
            if (record != null)
            {
                lstRCD01.RemoveAt(id);
            }
            return lstRCD01;
        }

        #endregion
    }
}
