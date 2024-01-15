using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryString.Models
{
    /// <summary>
    /// Defines company details
    /// </summary>
    public class CMP01
    {
        #region Public Members

        /// <summary>
        /// Declares id of company
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Declares name of company
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// Declares city of company
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Declares type of company
        /// </summary>
        public string P01F04 { get; set; }

        /// <summary>
        /// Declares number of employees in a company
        /// </summary>
        public int P01F05 { get; set; }

        #endregion

        #region Counstructors

        /// <summary>
        /// Creates object of CMP01 with id, name, city, type, number of employees
        /// </summary>
        /// <param name="id">Defines id of company</param>
        /// <param name="name">Defines nane of company</param>
        /// <param name="city">Defines city  of company</param>
        /// <param name="type">Defines type  of company</param>
        /// <param name="noEmp">Defines number of employees in company</param>
        public CMP01(int id, string name, string city, string type, int noEmp)
        {
            P01F01 = id;
            P01F02 = name;
            P01F03 = city;
            P01F04 = type;
            P01F05 = noEmp;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates list of companies
        /// </summary>
        /// <returns>List of companies</returns>
        public static List<CMP01> GetCompanies()
        {
            var list = new List<CMP01> {
                new CMP01(1,"TATA","Jamshedpur","Production",1028000),
                new CMP01(2,"Microsoft","Washington","IT",221000),
                new CMP01(3,"Google","California","IT",156000),
                new CMP01(4,"RKIT","Rajkot","IT",200),
                new CMP01(5,"Sugar Cosmetics","Mumbai","Production",600)
            };
            return list;
        }

        #endregion

    }
}