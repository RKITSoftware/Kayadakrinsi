using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Test.Database
{
    public class DLMOV01
    {
        #region Private Members

        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// Connection object of class MySqlConnection
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructors

        /// <summary>
        /// Establishes connection 
        /// </summary>
        public DLMOV01()
        {
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
        /// Selects movie's record from database
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            DataTable result = new DataTable();

            string query = String.Format(@"SELECT 
                                                V01F01, 
                                                V01F02, 
                                                V01F03, 
                                                V01F04,
                                                V01F05,
                                                V01F06
                                           FROM 
                                                MOV01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                result.Load(dataReader);

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }

        #endregion
    }
}