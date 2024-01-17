using System;
using System.Collections.Generic;
using System.Web.Http;
using Country.Models;

namespace Country.BusinessLogic
{
    /// <summary>
    /// Specifies logic for country api
    /// </summary>
    public class BLCountry
    {
        #region Public Members

        /// <summary>
        /// Maintains list of lstCountries
        /// </summary>
        public static List<CON01> lstCountries = new List<CON01> {
                    newUser(0, "India", 91, 1428),
                    newUser(1, "Australia", 61, 25.77)
         };

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="id">Id of country</param>
        /// <param name="name">country name</param>
        /// <param name="code">Country code</param>
        /// <param name="population">Population in million</param>
        /// <returns>object of USR01</returns>
        public static CON01 newUser(int id,string name,int code,double population){
            var objCON01 = new CON01();
            objCON01.N01F01 = id;
            objCON01.N01F02 = name;
            objCON01.N01F03 = code;
            objCON01.N01F04 = population;
            return objCON01;
        }

        /// <summary>
        /// Handles get request 
        /// </summary>
        /// <returns>list of countries</returns>
        public static List<CON01> GetCountries()
        {
            return lstCountries;
        }

        /// <summary>
        /// Handles get request for given id
        /// </summary>
        /// <param name="id">For searching particular country with given id</param>
        /// <returns>returns country with given id if exist</returns>
        public static CON01 GetById(int id)
        {
            var objCON01 = lstCountries.Find(x => x.N01F01 == id);
            return objCON01;
        }

        /// <summary>
        /// Handles post request
        /// </summary>
        /// <param name="objCON01">new country which is object of type Country</param>
        /// <returns>List of counties if request is ok</returns>
        public static List<CON01> AddCountry(CON01 objCON01)
        {
            var objCON01Temp = lstCountries.Find(x => x.N01F01 == objCON01.N01F01);
            if (objCON01Temp == null)
            {
                lstCountries.Add(objCON01);
                return lstCountries;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Handles put request
        /// </summary>
        /// <param name="objCON01">edited country which is also object of class Country</param>
        /// <returns>List of counties if request is ok</returns>
        public static List<CON01> EditCountry(CON01 objCON01)
        {
            var objCON01Temp = lstCountries.Find(x => x.N01F01 == objCON01.N01F01);
            if (objCON01Temp == null)
            {
                return null;
            }
            else
            {
                lstCountries[objCON01.N01F01] = objCON01;
                return lstCountries;
            }
        }

        /// <summary>
        /// Handles delete request
        /// </summary>
        /// <param name="id">id of element to be delete</param>
        /// <returns>List of counties if request is ok</returns>
        public static List<CON01> DeleteCountry(int id)
        {
            var objCON01 = lstCountries.Find(x => x.N01F01 == id);
            if (objCON01 == null)
            {
                return null;
            }
            else
            {
                lstCountries.RemoveAt(id);
                return lstCountries;
            }
        }


        #endregion

    }
}