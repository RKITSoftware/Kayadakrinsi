using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URI.Models
{
    /// <summary>
    /// Defines company details
    /// </summary>
    public class CMP02
    {
        #region Public Members

        /// <summary>
        /// Declares id of company
        /// </summary>
        public int P02F01 { get; set; }

        /// <summary>
        /// Declares name of company
        /// </summary>
        public string P02F02 { get; set; }

        /// <summary>
        /// Declares country of company
        /// </summary>
        public string P02F03 { get; set; }

        /// <summary>
        /// Declares type of company
        /// </summary>
        public string P02F04 { get; set; }

        /// <summary>
        /// Declares number of employees in a company
        /// </summary>
        public int P02F05 { get; set; }

        #endregion

        #region Counstructors

        /// <summary>
        /// Creates object of CMP02 with id, name, country, type, number of employees
        /// </summary>
        /// <param name="id">Defines id of company</param>
        /// <param name="name">Defines nane of company</param>
        /// <param name="city">Defines city  of company</param>
        /// <param name="type">Defines type  of company</param>
        /// <param name="noEmp">Defines number of employees in company</param>
        public CMP02(int id, string name, string country, string type, int noEmp)
        {
            P02F01 = id;
            P02F02 = name;
            P02F03 = country;
            P02F04 = type;
            P02F05 = noEmp;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates list of companies
        /// </summary>
        /// <returns>List of companies</returns>
        public static List<CMP02> GetCompanies()
        {
            var list = new List<CMP02> {
                new CMP02(1,"TATA","India","Production",1028000),
                new CMP02(2,"Microsoft","U.S.A.","IT",221000),
                new CMP02(3,"Google","U.S.A.","IT",156000),
                new CMP02(4,"RKIT","India","IT",200),
                new CMP02(5,"Sugar Cosmetics","India","Production",600)
            };
            return list;
        }

        #endregion

    }
}