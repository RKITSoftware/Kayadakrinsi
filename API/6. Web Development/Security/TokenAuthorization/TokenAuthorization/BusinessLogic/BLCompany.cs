using System.Collections.Generic;
using System.Linq;
using TokenAuthorization.Models;

namespace TokenAuthorization.BusinessLogic
{
    /// <summary>
    /// Defines business logic for company controller
    /// </summary>
    public class BLCompany
    {
        /// <summary>
        /// List of companies
        /// </summary>
        public static List<CMP01> lstCompanies = GetCompanies();

        /// <summary>
        /// Creates object of CMP01 with id, name, city, type, number of employees
        /// </summary>
        /// <param name="id">Defines id of company</param>
        /// <param name="name">Defines nane of company</param>
        /// <param name="city">Defines city  of company</param>
        /// <param name="type">Defines type  of company</param>
        /// <param name="noEmp">Defines number of employees in company</param>
        public static CMP01 NewCompany(int id, string name, string city, string type, int noEmp)
        {
            var objCMP01 = new CMP01();
            objCMP01.P01F01 = id;
            objCMP01.P01F02 = name;
            objCMP01.P01F03 = city;
            objCMP01.P01F04 = type;
            objCMP01.P01F05 = noEmp;
            return objCMP01;
        }
        /// <summary>
        /// Creates list of companies
        /// </summary>
        /// <returns>List of companies</returns>
        public static List<CMP01> GetCompanies()
        {
            var list = new List<CMP01>();
            list.Add(NewCompany(1, "TATA", "Jamshedpur", "Production", 1028000));
            list.Add(NewCompany(2, "Microsoft", "Washington", "IT", 221000));
            list.Add(NewCompany(3, "Google", "California", "IT", 156000));
            list.Add(NewCompany(4, "RKIT", "Rajkot", "IT", 200));
            list.Add(NewCompany(5, "Sugar Cosmetics", "Mumbai", "Production", 600));
            return list;
        }

        /// <summary>
        /// Handles request of user
        /// </summary>
        /// <returns>List of companies whoose id is less than three</returns>
        public static List<CMP01> GetCompanyById(int id)
        {
            return lstCompanies.Where(c => c.P01F01 == id).ToList();
        }

        /// <summary>
        ///  Handles request of admin
        /// </summary>
        /// <returns>List of companies whoose id is less than four</returns>
        public static List<CMP01> GetSomeCompanies()
        {
            return lstCompanies.Where(c => c.P01F01 <= 3).ToList();
        }

    }
}