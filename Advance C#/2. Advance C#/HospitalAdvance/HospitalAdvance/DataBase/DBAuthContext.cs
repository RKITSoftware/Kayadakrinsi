using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;

namespace HospitalAdvance.DataBase
{
    /// <summary>
    /// Contains method that uses database queries for PTN01
    /// </summary>
    public class DBAuthContext
    {

        #region Private Members

        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Connection object of class MySqlConnection
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Instance of BLCommonHandler class
        /// </summary>
        private BLCommonHandler _objBLCommonHandler = new BLCommonHandler();

        #endregion

        #region Constructors 

        /// <summary>
        /// Establishes connection with database
        /// </summary>
        public DBAuthContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// open connection to database
        /// </summary>
        /// <returns>true if connection opened else false</returns>
        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// close connection
        /// </summary>
        /// <returns>true if connection closed else false</returns>
        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Retrives all user from database
        /// </summary>
        /// <returns>List containing all object of USR01</returns>
        public List<USR01> GetUsers()
        {
            string query = String.Format(@"SELECT 
                                                R01F01,
                                                R01F02,
                                                R01F03,
                                                R01F04,
                                                R01F05
                                            FROM 
                                                USR01");

            List<USR01> lstusr01 = new List<USR01>();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    USR01 objUSR01 = new USR01();
                    objUSR01.R01F01 = (int)dataReader[0];
                    objUSR01.R01F02 = (string)dataReader[1];
                    string password = _objBLCommonHandler.DecryptAes((string)dataReader[2]);
                    objUSR01.R01F03 = password;
                    objUSR01.R01F04 = (enmUserRole)dataReader[3];
                    objUSR01.R01F05 = Convert.ToBoolean(dataReader[4]);
                    lstusr01.Add(objUSR01);
                }

                //close Data Reader
                dataReader.Close();

                BLUSR01Handler.CacheOperations("Users", lstusr01);

                //close Connection
                CloseConnection();
            }
            return lstusr01;

        }

        #endregion

    }
}
