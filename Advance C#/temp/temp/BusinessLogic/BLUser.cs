using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using temp.Models;

namespace temp.BusinessLogic
{
    public class BLUser
    {
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

        static BLUser()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

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

        public static string Create()
        {
            var query = "CREATE TABLE " +
                            "USR01" +
                        "(" +
                            "Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
                            "UserName VARCHAR(30)" +
                        ")";
            if (OpenConnection() == true)
            {
                using (var conn = _connection)
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    return "ok";
                }
            }
            return "oops";
        }

        public static string Insert(USR01 objUSR01) 
        { 
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert<USR01>(objUSR01);
                return "ok";
            }
        }
    }
}