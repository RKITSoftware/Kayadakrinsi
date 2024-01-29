using System.Collections.Generic;
using System.Text.Json;

namespace MusicCompany.BusinessLogic
{
    public class BLSerialize
    {
        public static string Serialize<T>(List<T> lst) where T : class
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize<IList<T>>(lst, options);
        }
    }
}