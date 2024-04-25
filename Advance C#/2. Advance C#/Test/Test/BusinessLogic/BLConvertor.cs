using System;
using System.Data;
using System.Reflection;

namespace Test.BusinessLogic
{
    /// <summary>
    /// Contains logic for converting data formats
    /// </summary>
    public class BLConvertor
    {
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
    }
}