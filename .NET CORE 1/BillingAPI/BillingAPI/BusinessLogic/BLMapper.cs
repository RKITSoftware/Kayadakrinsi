namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains mapper method
    /// </summary>
    public static class BLMapper
    {
        /// <summary>
        /// Maps properties from a source object to a target object.
        /// </summary>
        /// <typeparam name="Tdto">The type of the source object.</typeparam>
        /// <typeparam name="Tpoco">The type of the target object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>The target object with mapped properties.</returns>
        public static Tpoco Map<Tdto, Tpoco>(this Tdto source) where Tpoco : new()
        {
            var target = new Tpoco();

            // Iterate over properties of the source object
            foreach (var sourceProperty in typeof(Tdto).GetProperties())
            {
                // Find corresponding property in the target object
                var targetProperty = typeof(Tpoco).GetProperty(sourceProperty.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var value = sourceProperty.GetValue(source);
                    if (value != null)
                    {
                        // Check if the property types are compatible or assignable
                        if (targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                        {
                            targetProperty.SetValue(target, value);
                        }
                        else
                        {
                            // Try converting the value to the target property type
                            var convertedValue = Convert.ChangeType(value, targetProperty.PropertyType);
                            targetProperty.SetValue(target, convertedValue);
                        }
                    }
                }
            }

            return target;
        }
    }
}
