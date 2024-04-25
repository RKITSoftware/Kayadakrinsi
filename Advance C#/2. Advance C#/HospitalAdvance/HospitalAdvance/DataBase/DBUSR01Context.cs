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
    /// Contains database queries related to USR01
    /// </summary>
    public class DBUSR01Context
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
        public DBUSR01Context()
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
        /// Retrives all USR01 objects from database
        /// </summary>
        /// <returns>Datatable</returns>
		public DataTable SelectUsers()
        {
            DataTable dataTable = new DataTable();

            string query = String.Format(@"SELECT 
                                                R01F01,
                                                R01F02,
                                                R01F03,
                                                R01F04,
                                                R01F05
                                            FROM 
                                                USR01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                foreach (DataRow row in dataTable.Rows)
                {
                    // Example: Changing the value of a column
                    row["R01F03"] = _objBLCommonHandler.DecryptAes((string)row["R01F03"]);
                }

                //close Data Reader
                dataReader.Close();

                BLUSR01Handler.CacheOperations("Users", dataTable);

                //close Connection
                CloseConnection();
            }

            return dataTable;
        }

        /// <summary>
        /// Retrives all USR01 objects from database
        /// </summary>
        /// <returns>List of USR01 object</returns>
        public List<USR01> SelectList()
        {
            List<USR01> lstUSR01 = new List<USR01>();

            //use case When for enmRole

            string query = @"SELECT
                       R01F01,
                       R01F02,
                       R01F03,
                       R01F04,
                       R01F05,
                       R01F06,
                       R01F07,
                       R01F08
                    FROM
                        USR01";
            try
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand command = new MySqlCommand(query, _connection);

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        USR01 objUSR01 = new USR01();
                        objUSR01.R01F01 = (int)dataReader[0];
                        objUSR01.R01F02 = (string)dataReader[1];
                        objUSR01.R01F03 =  _objBLCommonHandler.DecryptAes(Convert.ToString(dataReader[2]));
                        objUSR01.R01F04 = (enmUserRole)Enum.Parse(typeof(enmUserRole), dataReader[3].ToString());
                        objUSR01.R01F05 = (enmIsActive)Enum.Parse(typeof(enmIsActive), dataReader[4].ToString());

                        if (!dataReader.IsDBNull(dataReader.GetOrdinal("R01F06")))
                        {
                            objUSR01.R01F06 = dataReader.GetInt32(dataReader.GetOrdinal("R01F06"));
                        }
                        else
                        {
                            objUSR01.R01F06 = null;
                        }

                        if (!dataReader.IsDBNull(dataReader.GetOrdinal("R01F07")))
                        {
                            objUSR01.R01F07 = dataReader.GetInt32(dataReader.GetOrdinal("R01F07"));
                        }
                        else
                        {
                            objUSR01.R01F07 = null;
                        }

                        if (!dataReader.IsDBNull(dataReader.GetOrdinal("R01F08")))
                        {
                            objUSR01.R01F08 = dataReader.GetInt32(dataReader.GetOrdinal("R01F08"));
                        }
                        else
                        {
                            objUSR01.R01F08 = null;
                        }

                        //// Parse enmUserRole
                        //if (int.TryParse(dataReader[3].ToString().Trim(), out int userRole))
                        //{
                        //    objUSR01.R01F04 = (Enums.enmUserRole)userRole;
                        //}

                        //// Parse enmIsActive
                        //if (int.TryParse(dataReader[4].ToString().Trim(), out int isActive))
                        //{
                        //    objUSR01.R01F05 = (Enums.enmIsActive)isActive;
                        //}

                        lstUSR01.Add(objUSR01);
                    }

                    dataReader.Close();

                    //close Connection
                    CloseConnection();
                }
                return lstUSR01;
            }
            catch(Exception ex)
            {
                CloseConnection();
                throw ex;
            }
           
        }

        /// <summary>
        /// Retrives next increment of R01F01 from database
        /// </summary>
        /// <returns>next increment id of USR01 table</returns>
        public int NextIncrement()
        {
            int nextIncrement = -1;

            string query = String.Format(@"SELECT 
                                        MAX(R01F01) AS R01F01
                                    FROM 
                                        {0}"
                                    , "usr01");

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                OpenConnection();
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                using (MySqlDataReader dataReader = command.ExecuteReader())
                {
                    // Check if there are any rows to read
                    if (dataReader.Read())
                    {
                        nextIncrement = Convert.ToInt32(dataReader["R01F01"]);
                    }
                }

                //close Connection
                CloseConnection();
            }

            return nextIncrement;
        }


        #endregion

    }
}