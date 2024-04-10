using System.Reflection;
using Logging.Models;
using NLog;
using NLog.Web;

namespace Logging.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLStationary controller
    /// </summary>
    public class BLStationary
    {

        #region Private Members

        /// <summary>
        /// Declares logger of type NLog.Logger
        /// </summary>
        private readonly Logger _logger;
        
        /// <summary>
        /// Declares object of result
        /// </summary>
        private static RES01 _objRES01;

        #endregion

        #region Public Members

        /// <summary>
        /// Count for auto increment id
        /// </summary>
        public static int idCount = 0;

        /// <summary>
        /// List of stationary items
        /// </summary>
        public static List<STA01> lstSTA01 = new List<STA01>();

        /// <summary>
        /// Instance of class BLConvertor
        /// </summary>
        public static BLConvertor objBLConvertor = new BLConvertor();

        /// <summary>
        /// Declares object of class STA01
        /// </summary>
        public static STA01 objSTA01;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public BLStationary()
        {
           _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates given object
        /// </summary>
        /// <returns>True if object is valid, false otherwise</returns>
        public bool Validation()
        {
            if (objSTA01 == null)
            {
                return false;
            }
            else if (objSTA01.A01F03 <= objSTA01.A01F04)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds new stationary item into the list
        /// </summary>
        /// <returns>Object of RES01</returns>
        public RES01 AddItem()
        {
            try
            {
                objSTA01.A01F01 = ++idCount;
                lstSTA01.Add(objSTA01);
                _objRES01 = new RES01 { isError = false, message = "Success" };
                return _objRES01;
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format(@"{0} | {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                _objRES01 = new RES01 { isError = true, message = ex.Message };
                return _objRES01;
            }
        }

        /// <summary>
        /// Updates stationary item into the list
        /// </summary>
        /// <returns>Object of RES01</returns>
        public RES01 UpdateItem()
        {
            try
            {
                var index = lstSTA01.FindIndex(i => i.A01F01 == objSTA01.A01F01);
                lstSTA01[index] = objSTA01;
                _objRES01 = new RES01 { isError = false, message = "Success" };
                return _objRES01;
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format(@"{0} | {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                _objRES01 = new RES01 { isError = true, message = ex.Message };
                return _objRES01;
            }
        }

        /// <summary>
        /// Deletes stationary item from the list
        /// </summary>
        /// <returns>Object of RES01</returns>
        public RES01 DelteteItem(int id)
        {
            try
            {
                var index = lstSTA01.FindIndex(i => i.A01F01 == id);
                lstSTA01.RemoveAt(index);
                _objRES01 = new RES01 { isError = false, message = "Success" };
                return _objRES01;
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format(@"{0} | {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                _objRES01 = new RES01 { isError = true, message = ex.Message };
                return _objRES01;
            }
        }

        /// <summary>
        ///Retrives stationary items from the list
        /// </summary>
        /// <returns>Object of RES01</returns>
        public RES01 GetItems()
        {
            _objRES01 = new RES01 { isError = false, message = "Ok", response = objBLConvertor.ToDataTable<STA01>(lstSTA01) };
            return _objRES01;
        }

        #endregion

    }
}
