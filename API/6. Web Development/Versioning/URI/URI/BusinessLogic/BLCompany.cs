using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using URI.Models;

namespace URI.BusinessLogic
{
    public class BLCompany
    {
        /// <summary>
        /// List of companies of type CMP02
        /// </summary>
        public static List<CMP02> lstCMP02 = new List<CMP02> 
        { 
            new CMP02{P02F01=1,P02F02="TATA",P02F03="India",P02F04="Production",P02F05=1028000},
            new CMP02{P02F01=2,P02F02="Microsoft",P02F03="U.S.A.",P02F04="IT",P02F05=221000},
            new CMP02{P02F01=3,P02F02="Google",P02F03="U.S.A.",P02F04="IT",P02F05=156000},
            new CMP02{P02F01=4,P02F02="RKIT",P02F03="India",P02F04="IT",P02F05=200},
            new CMP02{P02F01=5,P02F02="Sugar Cosmetics",P02F03="India",P02F04="",P02F05=600}
        };

        /// <summary>
        /// List of companies of type CMP02
        /// </summary>
        public static List<CMP01> lstCMP01 = new List<CMP01>
        {
            new CMP01{P01F01=1,P01F02="TATA",P01F03="Jamshedpur",P01F04="Production",P01F05=1028000},
            new CMP01{P01F01=2,P01F02="Microsoft",P01F03="California",P01F04="IT",P01F05=221000},
            new CMP01{P01F01=3,P01F02="Google",P01F03="California",P01F04="IT",P01F05=156000},
            new CMP01{P01F01=4,P01F02="RKIT",P01F03="Rajkot",P01F04="IT",P01F05=200},
            new CMP01{P01F01=5,P01F02="Sugar Cosmetics",P01F03="Mumbai",P01F04="Production",P01F05=600}
        };
    }
}