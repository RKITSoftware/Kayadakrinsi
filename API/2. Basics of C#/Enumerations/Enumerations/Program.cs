public class Program
{
    #region Private Members

    // Simple enum created
    enum enmRange
    {
        Small,
        Medium,
        Large,
    }

    // Enum defined using custom data type indices
    enum enmUserIds : short
    {
        Nidhi = 1,
        Vrundali = 2,
        Janvi = 30
    }

    #endregion

    #region Public Members

    // Enum as bit flags
    [Flags]
    public enum enmDays
    {
        None = 0b_0000_0000,  // 0
        Monday = 0b_0000_0001,  // 1
        Tuesday = 0b_0000_0010,  // 3
        Wednesday = 0b_0000_0100,  // 4
        Thursday = 0b_0000_1000,  // 8
        Friday = 0b_0001_0000,  // 16
        Saturday = 0b_0010_0000,  // 32
        Sunday = 0b_0100_0000,  // 64
        Weekend = Saturday | Sunday
    }
    #endregion

    #region Public Methods
    public static void Main(string[] args)
    {
        Console.WriteLine("Range small : " + (int)enmRange.Small);
        Console.WriteLine("Range medium : " + enmRange.Medium);
        Console.WriteLine("Range large : " + enmRange.Large + " Type : " + enmRange.Large.GetType());
        Console.WriteLine();

        Console.WriteLine("UserId of Nidhi : " + enmUserIds.Nidhi);
        Console.WriteLine("UserId of Vrundali : " + (int)enmUserIds.Vrundali);
        Console.WriteLine("UserId of Janvi : " + enmUserIds.Janvi + " Type : " + enmUserIds.Janvi.GetType());
        Console.WriteLine();

        enmDays meetingFreeDays = enmDays.Tuesday | enmDays.Wednesday | enmDays.Friday;
        Console.WriteLine(meetingFreeDays);
        var temp = (enmDays)3;
        Console.WriteLine(temp);
        
    }

    #endregion
}