using Newtonsoft.Json;
using System.Reflection;

namespace BillingAPI.BusinessLogic
{
     /// <summary>
     /// Contains logic for mapping DTO model to POCO model
     /// </summary>
     /// <typeparam name="Tdto">Type of DTO model</typeparam>
     /// <typeparam name="Tpoco">Type of POCO model</typeparam>
    public class BLMapper<Tdto, Tpoco> where Tdto : class where Tpoco : class, new()
    {
        /// <summary>
        /// Maps DTO model into POCO model
        /// </summary>
        /// <param name="objDto">Instance of DTO model</param>
        /// <returns>Instance of POCO model mapped from DTO model</returns>
        /// <exception cref="ArgumentNullException">Null Argument Exception</exception>
        public Tpoco Map(Tdto objDto)
        {
            if (objDto == null)
                throw new ArgumentNullException();

            Tpoco objPoco = new Tpoco();

            foreach (PropertyInfo dtoProperty in typeof(Tdto).GetProperties())
            {
                PropertyInfo pocoProperty = GetPropertyByJsonName(typeof(Tpoco), dtoProperty.Name);

                if (pocoProperty != null && pocoProperty.CanWrite)
                {
                    object value = dtoProperty.GetValue(objDto);
                    pocoProperty.SetValue(objPoco, value);
                }
            }

            return objPoco;
        }

        /// <summary>
        /// Retrives property of POCO model from JsonProperty attribute given in DTO model
        /// </summary>
        /// <param name="type">Type of model</param>
        /// <param name="jsonPropertyName">Name of JsonProperty</param>
        /// <returns>PropertyInfo of given JsonProperty</returns>
        public PropertyInfo GetPropertyByJsonName(Type type, string jsonPropertyName)
        {
            // Get all properties of the class
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                // Check if the property has a JsonProperty attribute
                JsonPropertyAttribute attribute = (JsonPropertyAttribute)property.GetCustomAttribute(typeof(JsonPropertyAttribute), true);

                if (attribute != null && attribute.PropertyName == jsonPropertyName)
                {
                    // Return the property if it matches the JSON property name
                    return property;
                }
            }

            // Return null if property not found
            return null;
        }
    }
}
