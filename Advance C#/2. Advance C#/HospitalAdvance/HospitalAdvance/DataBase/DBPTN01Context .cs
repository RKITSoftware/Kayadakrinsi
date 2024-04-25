using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;

namespace HospitalAdvance.DataBase
{
    /// <summary>
    /// Contains database queries related to PTN01
    /// </summary>
    public class DBPTN01Context
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
        public DBPTN01Context()
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
		/// Generates patient's charges details
		/// </summary>
		/// <param name="user">Current user</param>
		/// <returns>List of object</returns>
		public DataTable GetMyRecipt(USR01 user)
        {
            DataTable dataTable = new DataTable();

            string query = String.Format(@"SELECT
                                                D01.D01F01 AS RECORD_ID,
                                                N01.N01F02 AS PATIENT_NAME,
                                                F01.F01F02 AS DOCTOR_NAME,
                                                F02.F02F02 AS HELPER_NAME,
                                                S01.S01F02 AS DIEASES_NAME,
                                                D01.D01F07 AS ADMIT_DATE,
                                                D01.D01F08 AS DISCHARGE_DATE,
                                                D01.D01F09 AS TOTAL
                                            FROM
                                                RCD01 D01,
                                                PTN01 N01,
                                                PTN01 F01,
                                                STF02 F02,
                                                DIS01 S01,
                                            WHERE
                                                D01.D01F02 = N01.N01F01 AND
                                                D01.D01F03 = F01.F01F01 AND
                                                D01.D01F04 = F02.F02F01 AND
                                                D01.D01F05 = S01.S01F01 AND
                                                N01.N01F05 = {0}"
                                                , user.R01F01);

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                dataReader.Close();

                BLUSR01Handler.CacheOperations("DetailedRecords", dataTable);

                //close Connection
                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Retrives all PTN01 objects from database
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable Select()
        {
            DataTable dataTable = new DataTable();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               N01F01,
                                               N01F02,
                                               N01F03,
                                               S01F02 AS N01F04
                                            FROM
                                                PTN01
                                            JOIN
                                                DIS01
                                            ON 
                                                N01F04 = S01F01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                dataReader.Close();

                BLUSR01Handler.CacheOperations("Patients", dataTable);

                //close Connection
                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Retrives all PTN01 objects from database
        /// </summary>
        /// <returns>List of PTN01 objects</returns>
        public List<PTN01> SelectList()
        {
            List<PTN01> lstPTN01 = new List<PTN01>();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               N01F01,
                                               N01F02,
                                               N01F03,
                                               N01F04
                                            FROM
                                                PTN01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    PTN01 objPTN01 = new PTN01();
                    objPTN01.N01F01 = (int)dataReader[0];
                    objPTN01.N01F02 = (string)dataReader[1];
                    objPTN01.N01F03 = Convert.ToUInt64( dataReader[2]);
                    objPTN01.N01F04 = (int)dataReader[3];
                    lstPTN01.Add(objPTN01);
                }

                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstPTN01;
        }

        #endregion

    }
}