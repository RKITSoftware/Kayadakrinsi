namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains common logic
    /// </summary>
    public class BLCommon
    {
        /// <summary>
        /// Gets connection string from appsettings
        /// </summary>
        /// <returns>Connection string</returns>
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            // Replace "DefaultConnection" with the key used in appsettings.json for your connection string
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
