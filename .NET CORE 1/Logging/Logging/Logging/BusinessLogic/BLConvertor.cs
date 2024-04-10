using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using NLog;
using NLog.Web;

namespace Logging.BusinessLogic
{
    /// <summary>
    /// Represents convertor class logic
    /// </summary>
    public class BLConvertor
    {

        #region Private Members

        /// <summary>
        /// Declares logger of type NLog.Logger
        /// </summary>
        private readonly Logger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public BLConvertor()
        {
            _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts single object into datatable
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">Object to be convert</param>
        /// <returns>Datatable</returns>
        public DataTable ToDataTable<T>(T obj) where T : class 
        {
            DataTable dataTable = new DataTable();
            if (obj == null)
                return dataTable;

            Type objectType = typeof(T);
            PropertyInfo[] properties = objectType.GetProperties();

            // Create columns in DataTable based on object properties
            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            // Create a new row and set values for each property
            DataRow row = dataTable.NewRow();
            foreach (PropertyInfo property in properties)
            {
                row[property.Name] = property.GetValue(obj) ?? DBNull.Value;
            }
            dataTable.Rows.Add(row);

            _logger.Debug(String.Format(@"{0} | {1}", obj.GetType(), dataTable.GetType()));
            
            return dataTable;
        }

        /// <summary>
        /// Converts list into datatable
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="obj">List of type T</param>
        /// <returns>Datatable</returns>
        public DataTable ToDataTable<T>(List<T> obj) where T : class
        {
            string json = JsonConvert.SerializeObject(obj);
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);

            _logger.Debug(String.Format(@"{0} | {1}", obj.GetType(), dataTable.GetType()));

            return dataTable;
        }

        #endregion

    }
}
