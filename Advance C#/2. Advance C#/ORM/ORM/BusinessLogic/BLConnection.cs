using System.Configuration;
using ServiceStack.OrmLite;

namespace ORM.BusinessLogic
{
    /// <summary>
    /// Establishes connection to database
    /// </summary>
    public class BLConnection
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// Db factory
        /// </summary>
        public static OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
    }
}