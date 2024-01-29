using CRUD;
using MySql.Data.MySqlClient;

public class Program
{
    #region Private Members

    /// <summary>
    /// Connection string
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// Connection object of class MySqlConnection
    /// </summary>
    private readonly MySqlConnection _connection;

    #endregion

    #region Constructors

    /// <summary>
    /// Establishes connection 
    /// </summary>
    public Program()
    {
        _connectionString = @"server=127.0.0.1; user id=Admin; password=gs@123; database=sales";
        _connection = new MySqlConnection(_connectionString);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// open connection to database
    /// </summary>
    /// <returns>true if connection opened else false</returns>
    private bool OpenConnection()
    {
        try
        {
            _connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// close connection
    /// </summary>
    /// <returns>true if connection closed else false</returns>
    private bool CloseConnection()
    {
        try
        {
            _connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Creates table statement
    /// </summary>
    public void CreateTable()
    {
        string query = "CREATE TABLE IF NOT EXISTS ORD01" +
                        "(" +
                           "D01F01 INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'order id'," +
                           "D01F02 INT NOT NULL COMMENT 'product id'," +
                           "D01F03 VARCHAR(25) COMMENT 'product name'," +
                           "D01F04 INT DEFAULT 1 COMMENT 'quantity'," +
                           "D01F05 DECIMAL(10,2) COMMENT 'price'" +
                        ")";

        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand command = new MySqlCommand(query, _connection);

            //Execute command
            command.ExecuteNonQuery();
            Console.WriteLine("TABLE ok");

            //close connection
            this.CloseConnection();
        }
    }

    /// <summary>
    /// Insert statement
    /// </summary>
    /// <param name="objORD01">object of class ORD01</param>
    public void Insert(ORD01 objORD01)
    {
        string query = "INSERT INTO " +
                            "ORD01" +
                            "(D01F02,D01F03,D01F04,D01F05)" +
                        "VALUES" +
                            $"({objORD01.D01F02},'{objORD01.D01F03}',{objORD01.D01F04},{objORD01.D01F05})";

        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand command = new MySqlCommand(query, _connection);

            //Execute command
            command.ExecuteNonQuery();
            Console.WriteLine("INSERT ok");

            //close connection
            this.CloseConnection();
        }
    }

    /// <summary>
    /// Update statement
    /// </summary>
    public void Update()
    {
        string query = "UPDATE ORD01 SET D01F02=10 WHERE D01F03='CHOCOLATE'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand command = new MySqlCommand();
            //Assign the query using CommandText
            command.CommandText = query;
            //Assign the connection using Connection
            command.Connection = _connection;

            //Execute query
            command.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    /// <summary>
    /// Delete statement
    /// </summary>
    public void Delete()
    {
        string query = "DELETE FROM ORD01 WHERE D01F01=1";

        if (this.OpenConnection() == true)
        {
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    /// <summary>
    /// Select statement
    /// </summary>
    public void Select()
    {
        string query = "SELECT * FROM ORD01";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand command = new MySqlCommand(query, _connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = command.ExecuteReader();
            Console.WriteLine("D01F01, D01F02, D01F03, D01F04, D01F05");
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader[0]}, {dataReader[1]}, {dataReader[2]}, {dataReader[3]}, {dataReader[4]}");
            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();
        }
    }

    /// <summary>
    /// Drop statement
    /// </summary>
    public void Drop()
    {
        string query = "DROP TABLE ORD01";
        if(this.OpenConnection() == true)
        {
            MySqlCommand command = new MySqlCommand(query,_connection); 
            command.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    #endregion

    public static void Main(string[] args)
    {
       var objProgram = new Program();
        objProgram.CreateTable();
        objProgram.Insert(new ORD01 { D01F02 = 11, D01F03 = "CHOCOLATE", D01F04 = 5, D01F05 = 25.5 });
        objProgram.Insert(new ORD01 { D01F02 = 23, D01F03 = "BOOK", D01F04 = 2, D01F05 = 60 });
        objProgram.Insert(new ORD01 { D01F02 = 02, D01F03 = "SHOES", D01F04 = 1, D01F05 = 1000 });
        objProgram.Select();
        objProgram.Update();
        objProgram.Delete();
        objProgram.Drop();
    }
}