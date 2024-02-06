using System.Data;

internal class Program
{
    /// <summary>
    /// Creates data table for order detail
    /// </summary>
    /// <returns>data table</returns>
    public static DataTable CreateOrderDetailTable()
    {
        DataTable ORD01 = new DataTable("ORD01");

        // Defines all the columns once.
        DataColumn[] cols =
        {
            new DataColumn("D01F01", typeof(Int32)), // OrderDetailId
            new DataColumn("D01F02", typeof(String)), // Product
            new DataColumn("D01F03", typeof(Decimal)), // UnitPrice
            new DataColumn("D01F04", typeof(Int32)), // OrderQty
            new DataColumn("D01F05", typeof(Decimal), "D01F03*D01F04") // LineTotal
        };

        ORD01.Columns.AddRange(cols);
        ORD01.PrimaryKey = new DataColumn[] { ORD01.Columns["D01F01"] };
        return ORD01;
    }

    /// <summary>
    /// Inserts data into data table
    /// </summary>
    /// <param name="ORD01">data table in which data will be inserted</param>
    public static void InsertOrderDetails(DataTable ORD01)
    {
        // Used an Object array to insert all the rows .
        Object[] rows =
        {
            new Object[] { 1, "Mountain Bike", 1419.5, 36 },
            new Object[] { 2, "Road Bike", 1233.6, 16 },
            new Object[] { 3, "Touring Bike", 1653.3, 32 },
            new Object[] { 4, "Mountain Bike", 1419.5, 24 },
            new Object[] { 5, "Road Bike", 1233.6, 12 },
            new Object[] { 6, "Mountain Bike", 1419.5, 48 },
            new Object[] { 7, "Touring Bike", 1653.3, 8 },
        };

        foreach (Object[] row in rows)
        {
            ORD01.Rows.Add(row);
        }
    }

    /// <summary>
    /// Prints data
    /// </summary>
    /// <param name="data">Query result</param>
    public static void Print(EnumerableRowCollection data)
    {
        Console.WriteLine($"\nId, Product, Unit Price, Qunty., Total");
        foreach (DataRow row in data)
        {
            Console.WriteLine($"{row.Field<int>("D01F01")}, " +
                $"{row.Field<string>("D01F02")}, " +
                $"{row.Field<decimal>("D01F03")}, " +
                $"{row.Field<Int32>("D01F04")}, " +
                $"{row.Field<decimal>("D01F05")} ");
        }
    }

    /// <summary>
    /// Returns order details whoose total is in range of 20k-60k
    /// </summary>
    /// <param name="dt">data table</param>
    public static void QueryRange(DataTable dt)
    {
        var data = from row in dt.AsEnumerable()
                   where row.Field<decimal>("D01F05") < 60000
                   && row.Field<decimal>("D01F05") > 20000
                   select row;
        Print(data);
    }

    /// <summary>
    /// Returns data sorted by product name
    /// </summary>
    /// <param name="dt">data table</param>
    public static void QuerySort(DataTable dt)
    {
        var data = from row in dt.AsEnumerable()
                   orderby row.Field<string>("D01F02")
                   select row;
        Print(data);
    }

    /// <summary>
    /// Prints average unit price
    /// </summary>
    /// <param name="dt">data table</param>
    public static void QueryAverage(DataTable dt)
    {
        var data = dt.AsEnumerable().Average(row => row.Field<decimal>("D01F03"));

        Console.WriteLine($"\nAverage price : {System.Math.Round(data,2)}");
    }

    /// <summary>
    /// Prints maximum quantity 
    /// </summary>
    /// <param name="dt">data table</param>
    public static void QueryMax(DataTable dt)
    {
        var data = dt.AsEnumerable().Max(row => row.Field<Int32>("D01F04"));

        Console.WriteLine($"\nMax quantity : {data}");
    }

    /// <summary>
    /// Prints count whoose price in range of 1200-1350
    /// </summary>
    /// <param name="dt"></param>
    public static void QueryCount(DataTable dt)
    {
        var data = dt.AsEnumerable().Count(row => row.Field<decimal>("D01F03") > 1200 
                    && row.Field<decimal>("D01F03") < 1350);

        Console.WriteLine($"\nCount where price in range of 1200-1350 : {data}");
    }

    public static void Main(string[] args)
    {
        DataTable ORD01 = CreateOrderDetailTable();
        InsertOrderDetails(ORD01);
        QueryRange(ORD01);
        QuerySort(ORD01);
        QueryAverage(ORD01);
        QueryMax(ORD01);
        QueryCount(ORD01);
    }
}