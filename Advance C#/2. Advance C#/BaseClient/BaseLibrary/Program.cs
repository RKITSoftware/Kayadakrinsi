using System.Text;
public class CustomStringBuilder
{
    public static StringBuilder Append(String str)
    {
        StringBuilder objStringBuilder = new StringBuilder();
        objStringBuilder.Append(Encoding.UTF8.GetByteCount(str));
        return objStringBuilder;
    }
    public static StringBuilder Append(bool value)
    {
        StringBuilder objStringBuilder = new StringBuilder();
        objStringBuilder.Append(value ? "Value is true" : "Value is false");
        return objStringBuilder;
    }
    public static StringBuilder Clear(StringBuilder obj)
    {
        obj.Clear();
        Console.WriteLine($"String builder object has been cleared : {obj.Length}");
        return obj;
    }
}