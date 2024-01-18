using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Generics.Models;

namespace Generics.Models
{
    public class USR01
    {
        #region Public Members

        /// <summary>
        /// Id of user
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username of user
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        public string R01F04 { get; set; }

        #endregion

        
    }
}

//#region Constructors



//#endregion

//#region Public Methods

///// <summary>
///// Declares data of users
///// </summary>
///// <returns>List of users</returns>
//public static List<USR01> GetData()
//{
//    var data = new List<USR01> {
//                new USR01(1,"anmol","12345"),
//                new USR01(2,"geet","12345"),
//                new USR01(3,"raj","12345")
//            };
//    return data;
//}

//#endregion