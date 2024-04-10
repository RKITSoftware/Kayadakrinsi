using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.DataBase
{
    public class DL
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
		/// Declares Db factory instance
		/// </summary>
		private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors 

        public DL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
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
		/// Creates table statement
		/// </summary>
		/// <returns>Appropriate message</returns>
		public string CreateTable<T>() where T : class
        {
            using (var db = _dbFactory.OpenDbConnection())
            {

                if (!db.TableExists<T>())
                {
                    db.CreateTable<T>();
                }

                return "Table created successfully";
            }
        }

        /// <summary>
        /// Checks weather table exist or not in MySqlConnection
        /// </summary>
        /// <returns>True if table exists else false</returns>
        public bool TableExists(Type type)
        {
            try
            {
                string query = string.Format(@"SHOW TABLES LIKE '{0}'",type.Name);
                if (OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, _connection);

                    //Execute command
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        CloseConnection();
                        return true;
                    }
                    CloseConnection();
                }
                return false;
            }
            catch
            {
                CloseConnection();
                return false;
            }
        }

        /// <summary>
        /// Selects record of record with all details
        /// </summary>
        /// <returns>List of record details</returns>
        public dynamic SelectAllDetails()
        {
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
                STF01 F01,
                STF02 F02,
                DIS01 S01,
                CRG01 G01 
                WHERE 
                D01.D01F02 = N01.N01F01 AND 
                D01.D01F03 = F01.F01F01 AND
                D01.D01F04 = F02.F02F01 AND 
                D01.D01F05 = S01.S01F01 AND 
                D01.D01F06 = G01.G01F01 ORDER BY RECORD_ID");

            List<object> lstDetail = new List<object>();

            CloseConnection();
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

                BLUSR01.CacheOperations("DetailedRecords", lstDetail);

                //close Connection
                CloseConnection();
            }
            return lstDetail;
        }

        /// <summary>
		/// Generates patient's charges details
		/// </summary>
		/// <param name="user">Current user</param>
		/// <returns>List o object</returns>
		public dynamic GetMyRecipt(USR01 user)
        {
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
                STF01 F01,
                STF02 F02,
                DIS01 S01,
                CRG01 G01
                WHERE
                D01.D01F02 = N01.N01F01 AND
                D01.D01F03 = F01.F01F01 AND
                D01.D01F04 = F02.F02F01 AND
                D01.D01F05 = S01.S01F01 AND
                D01.D01F06 = G01.G01F01 AND N01.N01F05 = {0}",user.R01F01);

            List<object> lstDetail = new List<object>();

            CloseConnection();
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

                BLUSR01.CacheOperations("DetailedRecords", lstDetail);

                //close Connection
                CloseConnection();
            }
            return lstDetail;
        }

        /// <summary>
		/// Select statement
		/// </summary>
		/// <returns>List of users</returns>
		public List<USR01> SelectUsers()
        {
            if (!TableExists((typeof(USR01))))
            {
                CreateTable<USR01>();
            }
            string query = "SELECT * FROM USR01";

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
                    var objUSR01 = new USR01();
                    objUSR01.R01F01 = (int)dataReader[0];
                    objUSR01.R01F02 = (string)dataReader[1];
                    var password = BLSecurity.DecryptAes((string)dataReader[2], BLSecurity.key, BLSecurity.iv);
                    objUSR01.R01F03 = password;
                    objUSR01.R01F04 = (enmUserRole)dataReader[3];
                    lstusr01.Add(objUSR01);
                }

                //close Data Reader
                dataReader.Close();

                BLUSR01.CacheOperations("Users", lstusr01);

                //close Connection
                CloseConnection();
            }
            return lstusr01;
        }

        /// <summary>
        /// Generates next Id of USR01 table
        /// </summary>
        /// <returns>Next Id of USR01 table</returns>
        public int GetNextAutoIncrementNumber()
        {
            try
            {
                var query = "SELECT MAX(R01F01) FROM USR01";
                
                if (OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, _connection);

                    int nextAutoIncrementNumber = (int)command.ExecuteScalar() + 1;
                    
                    CloseConnection();

                    return nextAutoIncrementNumber;
                }

                return 0;
            }
            catch(Exception ex) 
            {
                CloseConnection();

                throw ex;
            }
        }

        /// <summary>
        /// Fetches discharge date of patient
        /// </summary>
        /// <param name="id">Id of patient</param>
        /// <returns>Discharge date</returns>
        public DateTime? getDischargeDate(int id)
        {
            try
            {
                var query = string.Format(@"SELECT 
                                                MAX(IF(D01F08!=NULL,NULL,D01F08))
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

                    if(dischargeDate != null )
                        return (DateTime)dischargeDate;
                }

                return null;
            }
            catch (Exception ex)
            {
                CloseConnection();

                throw ex;
            }
        }

        #endregion

    }
}