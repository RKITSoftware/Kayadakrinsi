using System.Collections.Generic;
using System.Linq;
using ExceptionFilter.Models;

namespace ExceptionFilter.BusinessLogic
{
    /// <summary>
    /// Defines business logic for company controller
    /// </summary>
    public class BLCompany
    {
        /// <summary>
        /// List of companies
        /// </summary>
        public static List<CMP01> lstCompanies = new List<CMP01>
        {
                new CMP01{P01F01=1, P01F02="TATA", P01F03="Jamshedpur", P01F04="Production", P01F05=1028000 },
                new CMP01{P01F01=2, P01F02 = "Microsoft", P01F03="Washington", P01F04="IT", P01F05=221000},
                new CMP01{P01F01=3, P01F02 = "Google", P01F03="California", P01F04="IT", P01F05=156000},
                new CMP01{P01F01=4, P01F02 = "RKIT", P01F03="Rajkot", P01F04="IT", P01F05=200},
                new CMP01{P01F01=5, P01F02 = "Sugar Cosmetics", P01F03="Mumbai", P01F04="Production",P01F05= 600 }
        };

        /// <summary>
        /// Handles request of user
        /// </summary>
        /// <returns>List of companies whoose id is less than three</returns>
        public List<CMP01> GetFewCompanies()
        {
            return lstCompanies.Where(c => c.P01F01 <= 2).ToList();
        }

        /// <summary>
        ///  Handles request of admin
        /// </summary>
        /// <returns>List of companies whoose id is less than four</returns>
        public List<CMP01> GetMoreCompanies()
        {
            return lstCompanies.Where(c => c.P01F01 <= 3).ToList();
        }

    }
}