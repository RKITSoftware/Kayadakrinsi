using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;

namespace HospitalAdvance.DataBase
{
    /// <summary>
    /// Contains database queries related to STF01
    /// </summary>
    public class DBSTF01Context
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
        public DBSTF01Context()
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
        /// Retrives all STF01 objects from database
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable Select()
        {
            DataTable dataTable = new DataTable();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               F01F01,
                                               F01F02,
                                               F01F03,
                                               F01F04
                                            FROM
                                                VWS_STF01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                dataReader.Close();

                BLUSR01Handler.CacheOperations("Doctors", dataTable);

                //close Connection
                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Retrives all STF01 objects from database
        /// </summary>
        /// <returns>List of STF01 object</returns>
        public List<STF01> SelectList()
        {
            List<STF01> lstSTF01 = new List<STF01>();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               F01F01,
                                               F01F02,
                                               F01F03,
                                               F01F04
                                            FROM
                                                STF01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    STF01 objSTF01 = new STF01();
                    objSTF01.F01F01 = (int)dataReader[0];
                    objSTF01.F01F02 = (string)dataReader[1];
                    objSTF01.F01F03 = (string)dataReader[2];
                    objSTF01.F01F04 = (enmDaysOfWeek)Enum.Parse(typeof(enmDaysOfWeek), dataReader[3].ToString());
                    lstSTF01.Add(objSTF01);
                }

                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstSTF01;
        }

        #endregion
    }
}