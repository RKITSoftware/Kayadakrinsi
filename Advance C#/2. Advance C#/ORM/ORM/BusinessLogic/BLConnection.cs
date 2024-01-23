using System.Configuration;
using ServiceStack.OrmLite;

namespace ORM.BusinessLogic
{
    /// <summary>
    /// Establishes connection to database
    /// </summary>
    public class BLConnection
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
    }
}