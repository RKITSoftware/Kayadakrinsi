using System.Data;
using NLog;
using NLog.Web;

namespace BillingAPI.BusinessLogic
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
        /// <typeparam name="Tdata">Type of object</typeparam>
        /// <param name="obj">Object to be convert</param>
        /// <returns>Datatable</returns>
        public DataTable ObjectToDataTable<T>(T obj) where T : class
        {
            DataTable dataTable = new DataTable();

            // Get all properties of the type T
            var properties = typeof(T).GetProperties();

            // Create columns in DataTable based on properties of T
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            // Create a DataRow and fill it with values from the object
            DataRow row = dataTable.NewRow();
            foreach (var prop in properties)
            {
                row[prop.Name] = prop.GetValue(obj);
            }

            // Add the DataRow to DataTable
            dataTable.Rows.Add(row);

            return dataTable;
        }
        
        /// <summary>
        /// Converts list into datatable
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="list">List of type T</param>
        /// <returns>Datatable</returns>
        public DataTable ListToDataTable<T>(List<T> list) where T : class
        {
            DataTable dataTable = new DataTable();

            if (list.Count == 0)
                return dataTable; // Return an empty DataTable if list is empty

            // Get all properties of the type T
            var properties = typeof(T).GetProperties();

            // Create columns in DataTable based on properties of T
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            // Fill DataTable with data from list
            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// Converts datatable into list
        /// </summary>
        /// <typeparam name="T">Type of List</typeparam>
        /// <param name="dataTable">Datatable to be convert</param>
        /// <returns>List of type T</returns>
        public List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            if (dataTable == null || dataTable.Rows.Count == 0)
                return list; // Return an empty list if DataTable is null or empty

            // Get all properties of the type T
            var properties = typeof(T).GetProperties();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();

                foreach (var prop in properties)
                {
                    // Check if the DataTable contains a column with the same name as the property
                    if (dataTable.Columns.Contains(prop.Name))
                    {
                        // Check if property type is an enumeration
                        if (prop.PropertyType.IsEnum)
                        {
                            // Convert the value from the DataTable to the enumeration type
                            var enumValue = Enum.Parse(prop.PropertyType, row[prop.Name].ToString());
                            prop.SetValue(obj, enumValue);
                        }
                        else
                        {
                            // Convert the value from the DataTable to the property type and set it
                            prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                        }
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        //public void ConvertUnsupportedTypes<T>(List<T> data) where T : class
        //{
        //    foreach (var item in data)
        //    {
        //        // Retrieve properties dynamically using reflection
        //        var properties = typeof(T).GetProperties();
        //        foreach (var property in properties)
        //        {
        //            if (property.PropertyType == typeof(Type))
        //            {
        //                // Convert the property value to string
        //                var value = property.GetValue(item);
        //                if (value != null)
        //                {
        //                    property.SetValue(item, value.ToString());
        //                }
        //            }
        //        }
        //    }
        //}


        #endregion

    }
}
