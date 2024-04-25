using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;
using ServiceStack;

namespace HospitalAdvance.DataBase
{
    /// <summary>
    /// Contains database queries related to STF02
    /// </summary>
    public class DBSTF02Context
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

        #endregion

        #region Constructors 

        /// <summary>
        /// Establishes connection with database
        /// </summary>
        public DBSTF02Context()
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
        /// Retrives all STF02 objects from database
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable Select()
        {
            DataTable dataTable = new DataTable();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               F02F01,
                                               F02F02,
                                               F02F03,
                                               F02F04
                                            FROM
                                                VWS_STF02");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                dataReader.Close();

                BLUSR01Handler.CacheOperations("Helpers", dataTable);

                //close Connection
                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Retrives all STF02 objects from database
        /// </summary>
        /// <returns>List of STF02 object</returns>
        public List<STF02> SelectList()
        {
            List<STF02> lstSTF02 = new List<STF02>();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               F02F01,
                                               F02F02,
                                               F02F03,
                                               F02F04
                                            FROM
                                                STF02");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    STF02 objSTF02 = new STF02();
                    objSTF02.F02F01 = (int)dataReader[0];
                    objSTF02.F02F02 = (string)dataReader[1];
                    objSTF02.F02F03= (enmRole)Enum.Parse(typeof(enmRole), dataReader[2].ToString());
                    objSTF02.F02F04 = (enmDaysOfWeek)Enum.Parse(typeof(enmDaysOfWeek), dataReader[3].ToString());
                    lstSTF02.Add(objSTF02);
                }

                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstSTF02;
        }

        #endregion

    }
}