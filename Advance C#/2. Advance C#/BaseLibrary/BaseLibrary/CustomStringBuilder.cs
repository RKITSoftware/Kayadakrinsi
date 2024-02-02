using System.Text;

/// <summary>
/// Custom Library
/// </summary>
public class CustomStringBuilder
{
    /// <summary>
    /// Implements Append method of base library inside custom library
    /// </summary>
    /// <param name="str">string to be append</param>
    /// <returns>string with latter's count</returns>
    public static StringBuilder Append(String str)
    {
        StringBuilder objStringBuilder = new StringBuilder();
        objStringBuilder.Append(str);
        objStringBuilder.Append(Encoding.UTF8.GetByteCount(str));
        return objStringBuilder;
    }

    /// <summary>
    /// Implements Append method of base library inside custom library
    /// </summary>
    /// <param name="value">boolean value</param>
    /// <returns>Appropriate message</returns>
    public static StringBuilder Append(bool value)
    {
        StringBuilder objStringBuilder = new StringBuilder();
        objStringBuilder.Append(value ? "Value is true" : "Value is false");
        return objStringBuilder;
    }

    /// <summary>
    /// Implements Clear method of base library inside custom library
    /// </summary>
    /// <param name="obj">string which will be cleared</param>
    /// <returns>Message</returns>
    public static StringBuilder Clear(StringBuilder obj)
    {
        obj.Clear();
        Console.WriteLine($"String builder object has been cleared : {obj.Length}");
        return obj;
    }
}