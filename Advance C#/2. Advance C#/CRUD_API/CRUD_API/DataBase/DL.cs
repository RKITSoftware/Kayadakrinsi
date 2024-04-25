using System;
using System.Collections.Generic;
using System.Linq;
using CRUD_API.Models;
using MySql.Data.MySqlClient;

namespace CRUD_API.DataBase
{
    /// <summary>
    /// Contains methods and other neccasary members to connect with database
    /// </summary>
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

        #endregion

        #region Constructors

        /// <summary>
        /// Establishes connection 
        /// </summary>
        public DL()
        {
            _connectionString = @"server=127.0.0.1; user id=Admin; password=gs@123; database=sales";
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
        /// Checks if table exists or not
        /// </summary>
        /// <returns></returns>
        public bool TableExists(Type type)
        {
            try
            {
                string query = string.Format(@"SHOW TABLES LIKE 
                                                '{0}'"
                                            , type.Name);

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
        /// Creates table statement
        /// </summary>
        public void CreateTable()
        {
            string query = String.Format(@"CREATE TABLE IF NOT EXISTS 
                                            ORD01
                                            (
                                               D01F01 INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'order id',
                                               D01F02 VARCHAR(25) COMMENT 'product name',
                                               D01F03 INT DEFAULT 1 COMMENT 'quantity',
                                               D01F04 DECIMAL(10,2) COMMENT 'price'
                                            )");

            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Execute command
                command.ExecuteNonQuery();
                Console.WriteLine("TABLE ok");

                //close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Insert statement
        /// </summary>
        /// <param name="ObjORD01">object of class ORD01</param>
        public string Insert(ORD01 ObjORD01)
        {
            string query = String.Format(@"INSERT INTO 
                                            ORD01
                                                (D01F02,D01F03,D01F04)
                                            VALUES
                                                ('{0}',{1},{2}) ",
                                           ObjORD01.D01F02, ObjORD01.D01F03, ObjORD01.D01F04);

            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Execute command
                command.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }

            return "Ok";
        }


        /// <summary>
        /// Update statement
        /// </summary>
        public string Update(int id,ORD01 ObjORD01)
        {
            var order = Select().FirstOrDefault(o => o.D01F01 == id);

            if (order != null)
            {
                string query = String.Format(@"UPDATE 
                                                    ORD01 
                                               SET  
                                                    D01F02='{0}',
                                                    D01F03={1},
                                                    D01F04={2}
                                               WHERE 
                                                    D01F01={3}"
                                               , ObjORD01.D01F02, ObjORD01.D01F03, ObjORD01.D01F04, id);

                //Open connection
                if (OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand command = new MySqlCommand();
                    //Assign the query using CommandText
                    command.CommandText = query;
                    //Assign the connection using Connection
                    command.Connection = _connection;

                    //Execute query
                    command.ExecuteNonQuery();

                    //close connection
                    CloseConnection();
                }
                return "Ok";
            }
            return "Error";
        }

        /// <summary>
        /// Delete statement
        /// </summary>
        public string Delete(int id)
        {
            string query = String.Format(@"DELETE FROM 
                                                ORD01 
                                            WHERE 
                                                D01F01={0}"
                                            , id);

            if (OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, _connection);

                command.ExecuteNonQuery();

                CloseConnection();
                
                return "Ok";
            }
            return "Error";
        }

        /// <summary>
        /// Select statement
        /// </summary>
        public List<ORD01> Select()
        {
            List<ORD01> lstORD01 = new List<ORD01>();

            string query = String.Format(@"SELECT 
                                                D01F01, 
                                                D01F02, 
                                                D01F03, 
                                                D01F04
                                           FROM 
                                                ORD01");

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
                    ORD01 ObjORD01 = new ORD01();

                    ObjORD01.D01F01 = (int)dataReader[0];
                    ObjORD01.D01F02 = (string)dataReader[1];
                    ObjORD01.D01F03 = (int)dataReader[2];
                    ObjORD01.D01F04 = Convert.ToDouble(dataReader[3]);

                    lstORD01.Add(ObjORD01);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstORD01;
        }

        /// <summary>
        /// Drop statement
        /// </summary>
        public string Drop()
        {
            string query = String.Format(@"DROP TABLE 
                                                ORD01");
            if (OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, _connection);
                command.ExecuteNonQuery();
                CloseConnection();
                return "Ok";
            }
            return "Error";
        }

        #endregion

    }
}