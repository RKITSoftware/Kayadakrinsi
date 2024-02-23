using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using MusicCompany.Models;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MusicCompany.BusinessLogic
{
    /// <summary>
    /// Handles logic for user controller
    /// </summary>
    public class BLUser
    {
        #region Private Members

        /// <summary>
        /// Connection string
        /// </summary>
        private static readonly string _connectionString;

        /// <summary>
        /// Connection object of class MySqlConnection
        /// </summary>
        private static readonly MySqlConnection _connection;

        /// <summary>
        /// Declares Db factory instance
        /// </summary>
        private readonly static IDbConnectionFactory _dbFactory;
    
        #endregion

        #region Constructors

        /// <summary>
        /// Establishes connection 
        /// </summary>
        static BLUser()
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
        private static bool OpenConnection()
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
        private static bool CloseConnection()
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
        public static string CreateTable()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {

                if (!db.TableExists<USR01>())
                {
                    db.CreateTable<USR01>();
                }

                return "Table created successfully";
            }
        }

        /// <summary>
        /// Checks weather table exist or not in MySqlConnection
        /// </summary>
        /// <returns>True if table exists else false</returns>
        public static bool TableExists()
        {
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand($"SHOW TABLES LIKE 'USR01'", _connection);

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
        /// Insert statement
        /// </summary>
        /// <param name="objUSR01">object of class USR01</param>
        /// <returns>Appropriate message</returns>
        public string Insert(USR01 objUSR01)
        {
            objUSR01.R01F03 = BLSecurity.EncryptAes(objUSR01.R01F03, BLSecurity.key, BLSecurity.iv);
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<USR01>())
                {
                    db.CreateTable<USR01>();
                }
                db.Insert(objUSR01);
                return "Success!";
            }

        }

        /// <summary>
        /// Update statement
        /// </summary>
        /// <param name="objUSR01">object of class USR01</param>
        /// <returns>Appropriate message</returns>
        public string Update(USR01 objUSR01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<USR01>())
                {
                    return "Table not exist";
                }
                var user = db.Select<USR01>().FirstOrDefault(u => u.R01F01 == objUSR01.R01F01);
                if (user == null)
                {
                    return "Choose valid record to edit!";
                }
                db.Update(objUSR01, u => u.R01F01 == objUSR01.R01F01);
                return "Success!";
            }
        }

        /// <summary>
        /// Delete statement
        /// </summary>
        /// <param name="id">User id to be delete</param>
        /// <returns>Appropriate message</returns>
        public string Delete(int id)
        {
            if (!TableExists())
            {
                return "Table not exist!";
            }
            string query = $"DELETE FROM USR01 WHERE R01F01={id}";

            if (OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, _connection);
                command.ExecuteNonQuery();
                CloseConnection();
                return "User deleted successfully";
            }
            return "Something went wrong";
        }

        /// <summary>
        /// Select statement
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> Select()
        {
            if (!TableExists())
            {
                CreateTable();
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
                    objUSR01.R01F04 = (string)dataReader[3];
                    lstusr01.Add(objUSR01);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstusr01;
        }

        /// <summary>
        /// Selects record of album with producer and artist details
        /// </summary>
        /// <returns>List of album details</returns>
        public dynamic SelectAllDetails()
        {
            string query = "SELECT " +
                "B01F01 AS ALBUM_ID," +
                "B01F02 AS ALBUM_NAME," +
                "B01F03 AS TOTAL_SONGS," +
                "O01F02 AS PRODUCER_NAME," +
                "O01F03 AS PRODUCING_COMPANY," +
                "T01F02 AS ARTIST " +
                "FROM ALB01 " +
                "JOIN " +
                "PRO01 " +
                "ON " +
                "ALB01.B01F04 = PRO01.O01F01 " +
                "JOIN " +
                "ART01 " +
                "ON " +
                "ALB01.B01F05=ART01.T01F01";

            //List<object>[] lstDetails = new List<object>[6];
            List<object> lstDetail = new List<object>();

            if (!TableExists())
            {
                return "Table not exist!";
            }
            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();


               while(dataReader.Read())
                {
                    lstDetail.Add(new
                    {
                       Album_Id = dataReader[0],
                       Album_name = dataReader[1],
                       TotalNoOfSongs = dataReader[2],
                       Producer_Name = dataReader[3],
                       Producing_Company = dataReader[4],
                       Artist_Name = dataReader[5]
                    });
                }

                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstDetail;
        }

		/// <summary>
		/// Seperates username and password from request
		/// </summary>
		/// <param name="Request">Current request</param>
		/// <returns>Username and password</returns>
		public static string[] GetUsernamePassword(HttpRequestMessage Request)
		{
			string authToken = Request.Headers.Authorization.Parameter;
			byte[] authBytes = Convert.FromBase64String(authToken);
			authToken = Encoding.UTF8.GetString(authBytes);
			string[] usernamepassword = authToken.Split(':');
			return usernamepassword;
		}
		#endregion
	}
} 