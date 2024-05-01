using System.Data;
using System.Reflection;
using BillingAPI.BusinessLogic;
using MySql.Data.MySqlClient;

namespace BillingAPI.Repositaries
{
    public class DBCommonContext<T> where T : class
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
        public DBCommonContext()
        {
            _connectionString = BLCommon.GetConnectionString();
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
        /// Retrives all object of type T
        /// </summary>
        /// <returns>Data table</returns>
        public DataTable Select()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            DataTable dataTable = new DataTable();

            string columns = string.Join(",", properties.Select(p => p.Name));

            string query = string.Format(@"SELECT
                                                {0}
                                           FROM
                                                {1}",
                                                columns, typeof(T).Name);

            if (_connection.State != ConnectionState.Open)
            {
                OpenConnection();

                MySqlCommand command = new MySqlCommand(query, _connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);

                dataReader.Close();

                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Retrives all object of type T
        /// </summary>
        /// <returns>List of object of type T</returns>
        public List<T> SelectList()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            string columns = string.Join(",", properties.Select(p => p.Name));

            string query = string.Format(@"SELECT
                                                {0}
                                           FROM
                                                {1}",
                                                columns, typeof(T).Name);


            if (_connection.State != ConnectionState.Open)
            {
                OpenConnection();

                MySqlCommand command = new MySqlCommand(query, _connection);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    T obj = Activator.CreateInstance<T>();

                    foreach (var property in properties)
                    {
                        if (!dataReader.IsDBNull(dataReader.GetOrdinal(property.Name)))
                        {
                            if (property.PropertyType.IsEnum)
                            {
                                // If the property is an enum, parse the string value from the database
                                // to the enum type and set it to the property
                                object enumValue = Enum.Parse(property.PropertyType, dataReader[property.Name].ToString());
                                property.SetValue(obj, enumValue);
                            }
                            else
                            {
                                // For non-enum properties, directly set the value from the database to the property
                                property.SetValue(obj, dataReader[property.Name]);
                            }
                        }
                    }

                    lst.Add(obj);
                }

                CloseConnection();
            }
            return lst;
        }

        #endregion

    }
}
