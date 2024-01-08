using System.Data;

public class CreatingDataTable 
{
    #region Public Methods

    // For creating 'Order' table
    public static DataTable CreateOrderTable()
    {
        DataTable orderTable = new DataTable("Order");

        // Defines one column.
        DataColumn colId = new DataColumn("OrderId", typeof(String));
        orderTable.Columns.Add(colId);

        DataColumn colDate = new DataColumn("OrderDate", typeof(DateTime));
        orderTable.Columns.Add(colDate);

        // Sets the OrderId column as the primary key.
        orderTable.PrimaryKey = new DataColumn[] { colId };

        return orderTable;
    }

    // For creating 'OrderDetail' table
    public static DataTable CreateOrderDetailTable()
    {
        DataTable orderDetailTable = new DataTable("OrderDetail");

        // Defines all the columns once.
        DataColumn[] cols =
        {
            new DataColumn("OrderDetailId", typeof(Int32)),
            new DataColumn("OrderId", typeof(String)),
            new DataColumn("Product", typeof(String)),
            new DataColumn("UnitPrice", typeof(Decimal)),
            new DataColumn("OrderQty", typeof(Int32)),
            new DataColumn("LineTotal", typeof(Decimal), "UnitPrice*OrderQty")
        };

        orderDetailTable.Columns.AddRange(cols);
        orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailTable.Columns["OrderDetailId"] };
        return orderDetailTable;
    }

    // For inserting data into 'Order' table
    public static void InsertOrders(DataTable orderTable)
    {
        // Adds one row once.
        DataRow row1 = orderTable.NewRow();
        row1["OrderId"] = "O0001";
        row1["OrderDate"] = new DateTime(2013, 3, 1);
        orderTable.Rows.Add(row1);

        DataRow row2 = orderTable.NewRow();
        row2["OrderId"] = "O0002";
        row2["OrderDate"] = new DateTime(2013, 3, 12);
        orderTable.Rows.Add(row2);

        DataRow row3 = orderTable.NewRow();
        row3["OrderId"] = "O0003";
        row3["OrderDate"] = new DateTime(2013, 3, 20);
        orderTable.Rows.Add(row3);
    }

    // For inserting data into 'OrderDetail' table
    public static void InsertOrderDetails(DataTable orderDetailTable)
    {
        // Used an Object array to insert all the rows .
        Object[] rows =
        {
            new Object[] { 1, "O0001", "Mountain Bike", 1419.5, 36 },
            new Object[] { 2, "O0001", "Road Bike", 1233.6, 16 },
            new Object[] { 3, "O0001", "Touring Bike", 1653.3, 32 },
            new Object[] { 4, "O0002", "Mountain Bike", 1419.5, 24 },
            new Object[] { 5, "O0002", "Road Bike", 1233.6, 12 },
            new Object[] { 6, "O0003", "Mountain Bike", 1419.5, 48 },
            new Object[] { 7, "O0003", "Touring Bike", 1653.3, 8 },
        };

        foreach (Object[] row in rows)
        {
            orderDetailTable.Rows.Add(row);
        }
    }

    // For displaying table
    public static void ShowTable(DataTable table)
    {
        foreach (DataColumn col in table.Columns)
        {
            Console.Write("{0,-14}", col.ColumnName);
        }
        Console.WriteLine();

        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn col in table.Columns)
            {
                if (col.DataType.Equals(typeof(DateTime)))
                    Console.Write("{0,-14:d}", row[col]);
                else if (col.DataType.Equals(typeof(Decimal)))
                    Console.Write("{0,-14}", row[col]);
                else
                    Console.Write("{0,-14}", row[col]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    #endregion


    #region Private Methods
    private static void Main(string[] args)
    {
        // Creates two tables and add them into the DataSet
        DataTable orderTable = CreateOrderTable();
        DataTable orderDetailTable = CreateOrderDetailTable();
        DataSet salesSet = new DataSet();
        salesSet.Tables.Add(orderTable);
        salesSet.Tables.Add(orderDetailTable);

        // Sets the relations between the tables and create the related constraint.
        salesSet.Relations.Add("OrderOrderDetail", orderTable.Columns["OrderId"], orderDetailTable.Columns["OrderId"], true);


        // Inserts the rows into the table
        InsertOrders(orderTable);
        InsertOrderDetails(orderDetailTable);

        Console.WriteLine("The initial Order table.");
        ShowTable(orderTable);

        Console.WriteLine("The OrderDetail table.");
        ShowTable(orderDetailTable);

        // Used the Aggregate-Sum on the child table column to get the result.
        DataColumn colSub = new DataColumn("SubTotal", typeof(Decimal), "Sum(Child.LineTotal)");
        orderTable.Columns.Add(colSub);

        // Computes the tax by referencing the SubTotal expression column.
        DataColumn colTax = new DataColumn("Tax", typeof(Decimal), "SubTotal*0.1");
        orderTable.Columns.Add(colTax);

        // If the OrderId is 'Total', computes the due on all orders; or computes the due on this order.
        DataColumn colTotal = new DataColumn("TotalDue", typeof(Decimal), "IIF(OrderId='Total',Sum(SubTotal)+Sum(Tax),SubTotal+Tax)");
        orderTable.Columns.Add(colTotal);

        DataRow row = orderTable.NewRow();
        row["OrderId"] = "Total";
        orderTable.Rows.Add(row);

        Console.WriteLine("The Order table with the expression columns.");
        ShowTable(orderTable);

    }

    #endregion

}
