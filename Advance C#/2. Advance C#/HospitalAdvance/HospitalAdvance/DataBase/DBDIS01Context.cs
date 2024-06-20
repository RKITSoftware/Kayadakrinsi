﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;

namespace HospitalAdvance.DataBase
{
    /// <summary>
    /// Contains database queries related to DIS01
    /// </summary>
    public class DBDIS01Context
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
        public DBDIS01Context()
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
        /// Retrives all DIS01 objects from database
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable Select()
        {
            DataTable dataTable = new DataTable();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               S01F01,
                                               S01F02,
                                               S01F03
                                            FROM
                                                DIS01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                dataReader.Close();

                BLUSR01Handler.CacheOperations("Dieases", dataTable);

                //close Connection
                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Retrives all DIS01 objects from database
        /// </summary>
        /// <returns>List of DIS01 objects</returns>
        public List<DIS01> SelectList()
        {
            List<DIS01> lstDIS01 = new List<DIS01>();

            //use case When for enmRole

            string query = String.Format(@"SELECT
                                               S01F01,
                                               S01F02,
                                               S01F03
                                            FROM
                                                DIS01");

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand command = new MySqlCommand(query, _connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    DIS01 objDIS01 = new DIS01();
                    objDIS01.S01F01 = (int)dataReader[0];
                    objDIS01.S01F02 = (string)dataReader[1];
                    objDIS01.S01F03 = Convert.ToDouble(dataReader[2]);
                    lstDIS01.Add(objDIS01);
                }

                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return lstDIS01;
        }

        #endregion
    }
}