using System;
using System.Collections.Generic;
using System.Configuration;
using HospitalAdvance.BusinessLogic;
using MySql.Data.MySqlClient;

namespace HospitalAdvance.DataBase
{
    /// <summary>
    /// Contains database queries related to RCD01
    /// </summary>
    public class DBRCD01Context
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
        public DBRCD01Context()
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
        /// Selects record of record with all details
        /// </summary>
        /// <returns>List of record details</returns>
        public dynamic SelectAllDetails()
        {
            string query = String.Format(@"SELECT
                                                RECORD_ID,
                                                PATIENT_NAME,
                                                DOCTOR_NAME,
                                                HELPER_NAME,
                                                DIEASES_NAME,
                                                ADMIT_DATE,
                                                DISCHARGE_DATE,
                                                TOTAL
                                            FROM 
                                                VWS_RCD01");

            List<object> lstDetail = new List<object>();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    lstDetail.Add(new
                    {
                        RECORD_ID = dataReader[0],
                        PATIENT_NAME = dataReader[1],
                        DOCTOR_NAME = dataReader[2],
                        HELPER_NAME = dataReader[3],
                        DIEASES_NAME = dataReader[4],
                        ADMIT_DATE = dataReader[5],
                        DISCHARGE_DATE = dataReader[6],
                        TOTAL = dataReader[7]
                    });
                }

                dataReader.Close();

                BLUSR01Handler.CacheOperations("DetailedRecords", lstDetail);

                //close Connection
                CloseConnection();
            }
            return lstDetail;
        }

        /// <summary>
        /// Fetches discharge date of patient
        /// </summary>
        /// <param name="id">Id of patient</param>
        /// <returns>Discharge date</returns>
        public DateTime getDischargeDate(int id)
        {

            string query = string.Format(@"SELECT 
                                                MAX(IFNULL(D01F06,NULL))
                                            FROM
                                                RCD01
                                            WHERE 
                                                D01F02 = {0}
                                            GROUP BY
                                                D01F02
                                            LIMIT 1", id);

            if (OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, _connection);

                var dischargeDate = command.ExecuteScalar();

                CloseConnection();

                return dischargeDate != null? Convert.ToDateTime(dischargeDate) : DateTime.MinValue;
            }

            return DateTime.MinValue;
        }

        #endregion

    }
}