using System.Data;

internal class Program
{
	/// <summary>
	/// For creating 'Order' table
	/// </summary>
	/// <returns>data table</returns>
	public DataTable CreateOrderTable()
	{
		DataTable ORD01 = new DataTable("ORD01");

		DataColumn colId = new DataColumn("D01F01", typeof(String)); // Order id
		ORD01.Columns.Add(colId);

		DataColumn colDate = new DataColumn("D01F02", typeof(DateTime)); // Order date
		ORD01.Columns.Add(colDate);

		ORD01.PrimaryKey = new DataColumn[] { colId };

		return ORD01;
	}

	/// <summary>
	/// Creates data table for order detail
	/// </summary>
	/// <returns>data table</returns>
	public DataTable CreateOrderDetailTable()
    {
        DataTable ORD02 = new DataTable("ORD02");

        // Defines all the columns once.
        DataColumn[] cols =
        {
            new DataColumn("D02F01", typeof(Int32)), // OrderDetailId
            new DataColumn("D02F02", typeof(String)), // Product
            new DataColumn("D02F03", typeof(Decimal)), // UnitPrice
            new DataColumn("D02F04", typeof(Int32)), // OrderQty
			new DataColumn("D02F05", typeof(String)), // Order id
            new DataColumn("D02F06", typeof(Decimal), "D02F03*D02F04") // LineTotal
		};

        ORD02.Columns.AddRange(cols);
        ORD02.PrimaryKey = new DataColumn[] { ORD02.Columns["D02F01"] };
        return ORD02;
    }

	/// <summary>
	/// For inserting data into 'Order' table
	/// </summary>
	/// <param name="ORD01">data table in which data will be inserted</param>
	public void InsertOrders(DataTable ORD01)
	{
		// Adds one row once.
		DataRow row1 = ORD01.NewRow();
		row1["D01F01"] = "O0001";
		row1["D01F02"] = new DateTime(2013, 3, 1);
		ORD01.Rows.Add(row1);

		DataRow row2 = ORD01.NewRow();
		row2["D01F01"] = "O0002";
		row2["D01F02"] = new DateTime(2013, 3, 12);
		ORD01.Rows.Add(row2);

		DataRow row3 = ORD01.NewRow();
		row3["D01F01"] = "O0003";
		row3["D01F02"] = new DateTime(2013, 3, 20);
		ORD01.Rows.Add(row3);

		DataRow row4 = ORD01.NewRow();
		row4["D01F01"] = "O0004";
		row4["D01F02"] = new DateTime(2013, 3, 25);
		ORD01.Rows.Add(row4);

		DataRow row5 = ORD01.NewRow();
		row5["D01F01"] = "O0005";
		row5["D01F02"] = new DateTime(2023, 12, 20);
		ORD01.Rows.Add(row5);

		DataRow row6 = ORD01.NewRow();
		row6["D01F01"] = "O0006";
		row6["D01F02"] = new DateTime(2015, 7, 18);
		ORD01.Rows.Add(row6);

		DataRow row7 = ORD01.NewRow();
		row7["D01F01"] = "O0007";
		row7["D01F02"] = new DateTime(2017, 1, 31);
		ORD01.Rows.Add(row7);
	}

	/// <summary>
	/// Inserts data into data table
	/// </summary>
	/// <param name="ORD02">data table in which data will be inserted</param>
	public void InsertOrderDetails(DataTable ORD02)
    {
        // Used an Object array to insert all the rows .
        Object[] rows =
        {
            new Object[] { 1, "Mountain Bike", 1419.5, 36 , "O0001"},
            new Object[] { 2, "Road Bike", 1233.6, 16 , "O0002" },
            new Object[] { 3, "Touring Bike", 1653.3, 32 ,"O0003"},
            new Object[] { 4, "Mountain Bike", 1419.5, 24 , "O0004" },
            new Object[] { 5, "Road Bike", 1233.6, 12 , "O0005" },
            new Object[] { 6, "Mountain Bike", 1419.5, 48 , "O0006"},
            //new Object[] { 7, "Touring Bike", 1653.3, 8 ,"O0007" },
        };

        foreach (Object[] row in rows)
        {
            ORD02.Rows.Add(row);
        }
    }

    /// <summary>
    /// Prints data
    /// </summary>
    /// <param name="data">Query result</param>
    public void Print(EnumerableRowCollection data)
    {
        Console.WriteLine($"\nId, Product, Unit Price, Qunty., Total");
        foreach (DataRow row in data)
        {
            Console.WriteLine($"{row.Field<int>("D02F01")}, " +
                $"{row.Field<string>("D02F02")}, " +
                $"{row.Field<decimal>("D02F03")}, " +
                $"{row.Field<Int32>("D02F04")}, " +
                $"{row.Field<decimal>("D02F06")} ");
        }
    }

    /// <summary>
    /// Returns order details whoose total is in range of 20k-60k
    /// </summary>
    /// <param name="dt">data table</param>
    public void QueryRange(DataTable dt)
    {
        var data = from row in dt.AsEnumerable()
                   where row.Field<decimal>("D02F06") < 60000
                   && row.Field<decimal>("D02F06") > 20000
                   select row;
        Print(data);
    }

    /// <summary>
    /// Returns data sorted by product name
    /// </summary>
    /// <param name="dt">data table</param>
    public void QuerySort(DataTable dt)
    {
        var data = from row in dt.AsEnumerable()
                   orderby row.Field<string>("D02F02")
                   select row;
        Print(data);
    }

    /// <summary>
    /// Prints average unit price
    /// </summary>
    /// <param name="dt">data table</param>
    public void QueryAverage(DataTable dt)
    {
        var data = dt.AsEnumerable().Average(row => row.Field<decimal>("D02F03"));

        Console.WriteLine($"\nAverage price : {System.Math.Round(data,2)}");
    }

    /// <summary>
    /// Prints maximum quantity 
    /// </summary>
    /// <param name="dt">data table</param>
    public void QueryMax(DataTable dt)
    {
        var data = dt.AsEnumerable().Max(row => row.Field<Int32>("D02F04"));

        Console.WriteLine($"\nMax quantity : {data}");
    }

    /// <summary>
    /// Prints count whoose price in range of 1200-1350
    /// </summary>
    /// <param name="dt"></param>
    public void QueryCount(DataTable dt)
    {
        var data = dt.AsEnumerable().Count(row => row.Field<decimal>("D02F03") > 1200 
                    && row.Field<decimal>("D02F03") < 1350);

        Console.WriteLine($"\nCount where price in range of 1200-1350 : {data}");
    }

    /// <summary>
    /// Performs inner join
    /// </summary>
    /// <param name="dt1">left table in join</param>
    /// <param name="dt2">right table in join</param>
    public void QueryInnerJoin(DataTable dt1,DataTable dt2)
    {
        var data = from t1 in dt1.AsEnumerable()
                   join t2 in dt2.AsEnumerable()
                   on t1.Field<String>("D01F01") equals t2.Field<String>("D02F05")
                   select new
                   {
                       OrderId = t2.Field<Int32>("D02F01"),
                       Product = t2.Field<String>("D02F02"),
                       UnitPrice = t2.Field<Decimal>("D02F03"),
                       Quantity = t2.Field<Int32>("D02F04"),
                       Date = t1.Field<DateTime>("D01F02"),
                       Total = t2.Field<Decimal>("D02F06")
                   };

        Console.WriteLine("\nInner join : ");
        Console.WriteLine("OrderId, Product, UnitPrice, Quantity, Date, Total");

        foreach(var element in data)
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",element.OrderId, element.Product, element.UnitPrice, 
                                element.Quantity, element.Date, element.Total);
        }
	}

    /// <summary>
    /// Performs left join
    /// </summary>
    /// <param name="args"></param>
    public void QueryLeftJoin(DataTable dt1,DataTable dt2)
    {
        var data = from t1 in dt1.AsEnumerable()
				   join t2 in dt2.AsEnumerable()
				   on t1.Field<String>("D01F01") equals t2.Field<String>("D02F05") into tempJoin
				   from leftJoin in tempJoin.DefaultIfEmpty()
				   select new
				   {
					   OrderId = leftJoin == null ? 0 : leftJoin.Field<Int32>("D02F01"),
					   Product = leftJoin == null ? "" : leftJoin.Field<String>("D02F02"),
					   UnitPrice = leftJoin == null ? 0 : leftJoin.Field<Decimal>("D02F03"),
					   Quantity = leftJoin == null ? 0 : leftJoin.Field<Int32>("D02F04"),
					   Date = t1.Field<DateTime>("D01F02"),
					   Total = leftJoin == null ? 0 : leftJoin.Field<Decimal>("D02F06")
				   };

	Console.WriteLine("\nLeft join : ");
        Console.WriteLine("OrderId, Product, UnitPrice, Quantity, Date, Total");

        foreach(var element in data)
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",element.OrderId, element.Product, element.UnitPrice, 
                                element.Quantity, element.Date, element.Total);
        }
    }

    public static void Main(string[] args)
    {
        Program objProgram = new Program();
		// Creates two tables and add them into the DataSet
		DataTable ORD01 = objProgram.CreateOrderTable();
		DataTable ORD02 = objProgram.CreateOrderDetailTable();
		DataSet salesSet = new DataSet();
		salesSet.Tables.Add(ORD01);
		salesSet.Tables.Add(ORD02);

		// Sets the relations between the tables and create the related constraint.
		salesSet.Relations.Add("OrderOrderDetail", ORD01.Columns["D01F01"], ORD02.Columns["D02F05"], true);

        objProgram.InsertOrders(ORD01);
		objProgram.InsertOrderDetails(ORD02);

        objProgram.QueryRange(ORD02);
        objProgram.QuerySort(ORD02);
        objProgram.QueryAverage(ORD02);
        objProgram.QueryMax(ORD02);
        objProgram.QueryCount(ORD02);
        objProgram.QueryInnerJoin(ORD01,ORD02);
		objProgram.QueryLeftJoin(ORD01, ORD02);
	}
}