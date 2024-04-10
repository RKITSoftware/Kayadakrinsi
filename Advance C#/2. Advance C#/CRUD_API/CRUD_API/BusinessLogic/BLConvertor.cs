using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace CRUD_API.BusinessLogic
{
    /// <summary>
    /// Represents convertor class logic
    /// </summary>
    public class BLConvertor
    {

        #region Public Methods

        /// <summary>
        /// Converts single object into datatable
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">Object to be convert</param>
        /// <returns>Datatable</returns>
        public DataTable ToDataTable<T>(T obj) where T : class
        {
            string json = JsonConvert.SerializeObject(obj);

            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);

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

            return dataTable;
        }

        #endregion

    }
}