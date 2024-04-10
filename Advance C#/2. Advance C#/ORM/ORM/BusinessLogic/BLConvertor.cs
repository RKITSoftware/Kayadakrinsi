using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace ORM.BusinessLogic
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



















///// <summary>
///// Converts an object to a DataTable
///// </summary>
///// <typeparam name="T">Type of object</typeparam>
///// <param name="obj">Object to be converted</param>
///// <returns>Datatable containing object data</returns>
//public DataTable ToDataTables<T>(T obj)
//{
//    DataTable dataTable = new DataTable();

//    // If obj is a single value type (e.g., int, string, etc.)
//    if (typeof(T).IsValueType || typeof(T) == typeof(string))
//    {
//        dataTable.Columns.Add("Value", typeof(T));
//        dataTable.Rows.Add(obj);
//    }


//    return dataTable;
//}