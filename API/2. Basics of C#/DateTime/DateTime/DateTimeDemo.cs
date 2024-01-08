/// <summary>
/// DateTimeDemo class is used to demonstrate how to create instance of 'DateTime' class as well as
/// how to implement it's methods and properties
/// </summary>
public class DateTimeDemo
{
    #region Public Methods

    public static void Main()
    {
        DateTime date = new DateTime(2022, 12, 20);
        DateTime dateTime = new DateTime(2023, 09, 16, 10, 12, 20);
        Console.WriteLine("Date : " + date);
        Console.WriteLine("Date time : " + dateTime);
        Console.WriteLine("Date " + dateTime.Date);
        Console.WriteLine("Day of month " + dateTime.Day + " Day of week " + dateTime.DayOfWeek + " Day of year " + dateTime.DayOfYear);
        Console.WriteLine("Hour " + dateTime.Hour + " Minute " + dateTime.Minute + " Second " + dateTime.Second);
        Console.WriteLine("Millisecond " + dateTime.Millisecond + " Tick " + dateTime.Ticks);
        Console.WriteLine("Year " + dateTime.Year+"\n");

        DateTime dateFormat = DateTime.Now;
        Console.WriteLine(dateFormat.ToString("MM/dd/yyyy"));
        Console.WriteLine(dateFormat.ToString("dddd, dd MMMM yyyy"));
        Console.WriteLine(dateFormat.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
        Console.WriteLine(dateFormat.ToString("MM/dd/yyyy HH:mm"));
        Console.WriteLine(dateFormat.ToString("MM/dd/yyyy hh:mm tt"));
        Console.WriteLine(dateFormat.ToString("MM/dd/yyyy H:mm"));
        Console.WriteLine(dateFormat.ToString("MM/dd/yyyy h:mm tt"));
        Console.WriteLine(dateFormat.ToString("MM/dd/yyyy HH:mm:ss"));
        Console.WriteLine(dateFormat.ToString("MMMM dd"));
        Console.WriteLine(dateFormat.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK"));
        Console.WriteLine(dateFormat.ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’"));
        Console.WriteLine(dateFormat.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"));
        Console.WriteLine(dateFormat.ToString("HH:mm"));
        Console.WriteLine(dateFormat.ToString("hh:mm tt"));
        Console.WriteLine(dateFormat.ToString("H:mm"));
        Console.WriteLine(dateFormat.ToString("h:mm tt"));
        Console.WriteLine(dateFormat.ToString("HH:mm:ss"));
        Console.WriteLine(dateFormat.ToString("yyyy MMMM")+"\n");


        TimeSpan duration = new TimeSpan(36, 0, 0, 0);
        DateTime modifiedDateTime = dateTime.Add(duration);

        Console.WriteLine("Date after adding timespan : " + modifiedDateTime);
        Console.WriteLine("Compare : " + dateTime.CompareTo(date));
        Console.WriteLine("Is equal : " + dateTime.Equals(date));
        Console.WriteLine("Add day : " + dateTime.AddDays(1));
        Console.WriteLine("String : " + date.ToString() + " Type : " + date.ToString().GetType());
    }

    #endregion

}
