using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Country.Models
{
    /// <summary>
    /// Defines details of country
    /// </summary>
    public class CON01
    {
        #region Public Members

        /// <summary>
        /// Declares ID of Country
        /// </summary>
        public int N01F01 { get; set; }

        /// <summary>
        /// Declares name of Country
        /// </summary>
        public string N01F02 { get; set; }

        /// <summary>
        /// Declares country code of particular Country
        /// </summary>
        public int N01F03 { get; set; }

        /// <summary>
        /// Declares population of County in millions
        /// </summary>
        public Double N01F04 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates object of Country with id, name, code, population
        /// </summary>
        /// <param name="id">id of country</param>
        /// <param name="name">name of country</param>
        /// <param name="code">country code of counatry</param>
        /// <param name="populationInM">population of country</param>
        public CON01(int id, string name, int code, double populationInM)
        {
            N01F01 = id;
            N01F02 = name;
            N01F03 = code;
            N01F04 = populationInM;
        }

        #endregion
    }
}