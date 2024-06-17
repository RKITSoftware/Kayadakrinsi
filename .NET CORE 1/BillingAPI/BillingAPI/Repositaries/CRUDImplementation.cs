using BillingAPI.BusinessLogic;
using BillingAPI.Enums;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using ServiceStack.OrmLite;

namespace BillingAPI.Repositaries
{
    /// <summary>
    /// Impelements ICommonService interface
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class CRUDImplementation<T> : ICRUDService<T> where T : class
    {
        #region Private Members

        /// <summary>
        /// Instance of OrmLiteConnectionFactory
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Instance of IConfiguration
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Public Members

        /// <summary>
        /// Declares variable to store connection string
        /// </summary>
        public static string connectionString;

        /// <summary>
        /// Instance of type T
        /// </summary>
        public T obj { get; set; }

        /// <summary>
        /// Type of operation to be perform
        /// </summary>
        public enmOperations Operations {get; set; }

        /// <summary>
        /// Instance of DBCommonContext DB context class
        /// </summary>
        public DBCommonContext<T> objDBCommonContext = new DBCommonContext<T>();

        #endregion

        #region Constructors

        /// <summary>
        /// Establishes connection with database
        /// </summary>
        //public CRUDImplementation(IConfiguration config)
        //{
        //    _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        //    _config = config;
        //    connectionString = BLCommon.GetConnectionString();
        //    //connectionString = _config["ConnectionStrings:DefaultConnection"];
        //}
        
        /// <summary>
        /// Establishes connection with database
        /// </summary>
        public CRUDImplementation()
        {
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            connectionString = BLCommon.GetConnectionString();
            //connectionString = _config["ConnectionStrings:DefaultConnection"];
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks weather object exist or not
        /// </summary>
        /// <param name="id">Id of object to be check</param>
        /// <returns>True if exist, False otherwise</returns>
        public bool IsExists(int id)
        {
            T obj;

            using (var db = _dbFactory.OpenDbConnection())
            {
                obj = db.SingleById<T>(id);
            }

            return obj == null ? false : true;
        }

        /// <summary>
        /// Retrives all objects of type T
        /// </summary>
        /// <returns>Response containing all objects of type T</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                response.response = objDBCommonContext.Select();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Retrieves all objects of type T
        /// </summary>
        /// <returns>List containing all objects of type T</returns>
        public List<T> SelectList()
        {
            List<T> lst = objDBCommonContext.SelectList();

            return lst;
        }

        /// <summary>
        /// Save object into the database
        /// </summary>
        /// <returns>Response containing value of Save operation</returns>
        public Response Save()
        {
            Response response = new Response();

            try
            {
                if (Operations == enmOperations.I)
                {
                    using (var db = _dbFactory.OpenDbConnection())
                    {
                        db.Insert<T>(obj);
                    }

                    response.message = "Inserted successfully";
                }
                else if (Operations == enmOperations.U)
                {
                    using (var db = _dbFactory.OpenDbConnection())
                    {
                        db.Update<T>(obj);
                    }

                    response.message = "Updated successfully";
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes object from database
        /// </summary>
        /// <param name="id">Id of object to be delete</param>
        /// <returns>Response containing value of Delete operation</returns>
        public Response Delete(int id)
        {
            Response response = new Response();

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {

                    db.DeleteById<T>(id);

                    response.message = "Deleted Successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
