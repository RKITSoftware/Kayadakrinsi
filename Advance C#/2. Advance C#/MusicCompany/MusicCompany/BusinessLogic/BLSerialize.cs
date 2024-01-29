using System.Collections.Generic;
using System.Text.Json;

namespace MusicCompany.BusinessLogic
{
    /// <summary>
    /// Logic for serializing data
    /// </summary>
    public class BLSerialize
    {
        /// <summary>
        /// Serializes different objects into string
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <param name="lst">List of objects</param>
        /// <returns>Serialized string</returns>
        public static string Serialize<T>(List<T> lst) where T : class
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize<IList<T>>(lst, options);
        }
    }
}