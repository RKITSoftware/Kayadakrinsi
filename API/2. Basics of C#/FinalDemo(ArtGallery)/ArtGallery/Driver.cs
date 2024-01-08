using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

public class ArtGallery
{
    #region Public Methods

    /// <summary>
    /// Logs everytime when a new item is added.
    /// </summary>
    /// <param name="art">It is a object of appropriate class based on category.</param>
    public static void ArtLog(dynamic art)
    {
        string path = @"C:\Users\KRINSHI\OneDrive\Documents\RKIT\C#\ArtGallery\ArtGallery\text\ArtLog.txt";
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(art.category + " is added by " + art.ArtistName + " on " + art.CreationDate + "\n");
            }
        }
        else
        {
            File.AppendAllText(path, art.category + " is added by " + art.ArtistName + " on " + art.CreationDate + "\n");
        }
    }

    /// <summary>
    /// Creates data table of art with details.
    /// </summary>
    /// <returns>Data table named artDetails</returns>
    public static DataTable CreateArtDetails()
    {
        DataTable artDetails = new DataTable("ArtDetails");
        DataColumn column = new DataColumn("ArtId", typeof(Int32));
        column.AutoIncrement = true;
        column.AutoIncrementSeed = 1;
        column.AutoIncrementStep = 1;
        artDetails.Columns.Add(column);
        DataColumn[] cols =
        {
            new DataColumn("Name",typeof(String)),
            new DataColumn("Artist",typeof(string)),
            new DataColumn("Category",typeof(String)),
            new DataColumn("CreationDate",typeof(DateTime)),
            new DataColumn("IsSold",typeof(bool)),
            new DataColumn("Price",typeof(long)),
        };
        artDetails.Columns.AddRange(cols);
        artDetails.PrimaryKey = new DataColumn[] { artDetails.Columns["ArtId"] };
        return artDetails;
    }

    /// <summary>
    /// Displays data table.
    /// </summary>
    /// <param name="table">Takes data table which will be displayed</param>
    public static void ShowTable(DataTable table)
    {
        foreach (DataColumn col in table.Columns)
        {
            Console.Write("{0,-18}", col.ColumnName);
        }
        Console.WriteLine();

        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn col in table.Columns)
            {
                if (col.DataType.Equals(typeof(DateTime)))
                    Console.Write("{0,-18:d}", row[col]);
                else if (col.DataType.Equals(typeof(Decimal)))
                    Console.Write("{0,-18}", row[col]);
                else
                    Console.Write("{0,-18}", row[col]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Inserts new art record in data table artDetails.
    /// </summary>
    /// <param name="artDetails">Data table in which record will be inserted</param>
    /// <param name="art">Details of record of type object according to it's class</param>
    public static void AddRow(DataTable artDetails,dynamic art)
    {
        DataRow row = artDetails.NewRow();
        row["Name"] = art.Name;
        row["Artist"] = art.ArtistName;
        row["Category"] = art.category;
        row["CreationDate"] = art.CreationDate;
        row["IsSold"] = art.IsSold;
        row["Price"] = art.Price;
        artDetails.Rows.Add(row);
    }

    /// <summary>
    /// Take details from user and inserts it into data table.
    /// </summary>
    /// <param name="artDetails">Data table in which record will be inserted</param>
    /// <param name="option">Option choosen by user based on new art's category</param>
    public static void InsertArt(DataTable artDetails,int option)
    {
        string name;
        string artistName;
        DateTime creationDate;
        bool isSold;
        long price;

        Console.WriteLine("\nEnter name : ");
        name = Console.ReadLine();
        Console.WriteLine("Enter artist's name : ");
        artistName = Console.ReadLine();
        Console.WriteLine("Enter date of creation (yyyy-MM-dd HH:mm) : ");
        String dt = Console.ReadLine();
        creationDate=DateTime.Parse(dt);
        Console.WriteLine("Is sold ? (true or false) ");
        isSold = Console.ReadLine() != null;
        Console.WriteLine("Enter price : ");
        price = Convert.ToInt32(Console.ReadLine());

        switch (option) 
        {   
            case 1:
                Arts newArt = new Arts(name,artistName, creationDate, isSold, price);
                AddRow(artDetails, newArt);
                ArtLog(newArt);
                break;
            case 2:
                RasinArt newRasinArt = new RasinArt(name,artistName, creationDate, isSold, price);
                AddRow(artDetails, newRasinArt);
                ArtLog(newRasinArt);
                break;
            case 3:
                Paintings newPainting = new Paintings(name,artistName, creationDate, isSold, price);
                AddRow(artDetails, newPainting);
                ArtLog(newPainting);
                break;
            case 4:
                Murals newMural = new Murals(name,artistName, creationDate, isSold, price);
                AddRow(artDetails, newMural);
                ArtLog(newMural);
                break;
        }
    }
    /// <summary>
    /// Run in loop and stop when exit is selected.
    /// </summary>
    public static void Main()
    {
        int flag = 1;
        DataTable artDetails = CreateArtDetails();

        while (flag==1)
        {
            try
            {
                int choice;
                Console.WriteLine("\nEnter your choice : \n1.Add art peice\n2.Show arts\n3.Exit\n");
                choice = Convert.ToInt32(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        int option;
                        Console.WriteLine("Choose category : \n1.Art\n2.Rasin Art\n3.Painting\n4.Murals");
                        option = Convert.ToInt32(Console.ReadLine());
                        InsertArt(artDetails,option);
                        break;
                    case 2:
                        Console.WriteLine("Arts details are as follows : \n");
                        ShowTable(artDetails);
                        break;
                    case 3:
                        flag = 0;
                        Console.WriteLine("Thank You!");
                        break;
                    default: 
                        Console.WriteLine("Enter valid choice!");
                        break;
                }
            }
            catch (Exception e) { 
                Console.WriteLine(e.ToString()); 
            }
        }
    }

    #endregion

}
