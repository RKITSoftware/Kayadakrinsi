using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using HospitalAdvance.Auth;
using HospitalAdvance.DataBase;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using Newtonsoft.Json.Linq;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for CLUSR01 controller
    /// </summary>
    public class BLUSR01Handler
    {

        #region Private Members

        /// <summary>
        /// Declares object of class Cache
        /// </summary>
        private static Cache _objCache = new Cache();

        /// <summary>
        /// Declares Db factory instance
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Public Members

        /// <summary>
        /// Instance of operation type
        /// </summary>
        public enmOperations ObjOperations { get; set; }

        /// <summary>
        /// Instance of USR01 class
        /// </summary>
        public USR01 ObjUSR01 { get; set; }

        /// <summary>
        /// Instance of BLCommonHandler class
        /// </summary>
        public BLCommonHandler objBLCommonHandler = new BLCommonHandler();

        /// <summary>
        /// Instance DBUSR01Context of DB context class
        /// </summary>
       public DBUSR01Context objDBUSR01Context = new DBUSR01Context();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes db Factory instance
        /// </summary>
        public BLUSR01Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        // common cache

        /// <summary>
        /// Performs caching operations
        /// </summary>
        /// <param name="key">Key of cache</param>
        /// <param name="obj">Value</param>D/
        public static void CacheOperations(string key, object obj)
        {
            var inCache = _objCache.Get(key);
            if (inCache == null)
            {
                _objCache.Insert(key, obj);
            }
            else
            {
                _objCache.Remove(key);
                _objCache.Insert(key, obj);
            }

        }

        /// <summary>
        /// Gives all cache items
        /// </summary>
        /// <returns>List of cache items</returns>
        public Dictionary<string, object> GetCache()
        {

            Dictionary<string, object> cacheItems = new Dictionary<string, object>();

            // Get all cache keys
            IDictionaryEnumerator cacheKeys = _objCache.GetEnumerator();

            // Iterate through each cache item and add it to the list
            while (cacheKeys.MoveNext())
            {
                string key = cacheKeys.Key.ToString();
                object cacheItem = _objCache[key];

                // Create a Cache object and add it to the list
                cacheItems.Add(key, cacheItem);
            }

            return cacheItems;
        }

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="name">Name of user(not username)</param>
        /// <param name="role">Role of user</param>
        /// <param name="id">Foreign key reference</param>
        public void PreSave(string name, enmUserRole role, int id)
        {
            int nextIncrement = objDBUSR01Context.NextIncrement() + 1;

            ObjUSR01 = new USR01
            {
                R01F02 = name + nextIncrement,
                R01F03 = objBLCommonHandler.EncryptAes("pswd" + name + nextIncrement),
                R01F04 = role
            };

            switch (role)
            {
                case enmUserRole.M:
                    break;
                case enmUserRole.D:
                    ObjUSR01.R01F06 = id;
                    break;
                case enmUserRole.H:
                    ObjUSR01.R01F07 = id;
                    break;
                case enmUserRole.P:
                    ObjUSR01.R01F08 = id;
                    break;
            }
        }

        /// <summary>
        /// Checks weather object of USR01 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Create a SQL expression
                var expression = db.From<USR01>()
                                    .Where(u => u.R01F01 == id && u.R01F05 == enmIsActive.Y);

                // Execute the query and check if any records match the criteria
                return db.Exists(expression);
            }
        }

        /// <summary>
        /// Validates the object before saving or updating
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response Validation()
        {
            Response response = new Response();

            string passwordPattern = "^(?=.*[a-z])(?=.*\\d).{8,}$";

            if (!Regex.IsMatch(ObjUSR01.R01F03, passwordPattern))
            {
                response.isError = true;
                response.message = "Password should be of minimun eight characters containing small letters and digits only";
            }
            else if (ObjOperations == enmOperations.I)
            {
                if (IsExist(ObjUSR01.R01F01))
                {
                    response.isError = true;
                    response.message = "User already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjUSR01.R01F01))
                {
                    response.isError = true;
                    response.message = "No User found";
                }
            }

            return response;
        }

        /// <summary>
        /// Retrieves all USR01 objects
        /// </summary>
        /// <returns>Response containing all USR01 objects</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                DataTable dataTable = objDBUSR01Context.SelectUsers();

                if (dataTable.Rows.Count > 0)
                {
                    response.response = dataTable;
                }
                else
                {
                    response.isError = true;
                    response.message = "No data found";
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrives current logged in user's object
        /// </summary>
        /// <returns>Object of USR01</returns>
        public USR01 GetUser()
        {
            string token = BLTokenHandler.cache.Get("JWTToken_" + BasicAuthentication.objUSR01.R01F02).ToString();

            string jwtEncodedPayload = token.Split('.')[1];

            // pad jwtEncodedPayload
            jwtEncodedPayload = jwtEncodedPayload.Replace('+', '-')
                                                 .Replace('/', '_')
                                                 .Replace("=", "");

            int padding = jwtEncodedPayload.Length % 4;
            if (padding != 0)
            {
                jwtEncodedPayload += new string('=', 4 - padding);
            }

            // decode the jwt payload
            byte[] decodedPayloadBytes = Convert.FromBase64String(jwtEncodedPayload);

            string decodedPayload = Encoding.UTF8.GetString(decodedPayloadBytes);

            JObject json = JObject.Parse(decodedPayload);

            USR01 user = objDBUSR01Context.SelectList().FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());

            return user;

        }

        #endregion

    }
}





















///// <summary>
///// Saves the USR01 object
///// </summary>
///// <returns>Response indicating the result of the save operation</returns>
//public Response Save()
//{
//    Response response = new Response();

//    try
//    {
//        using (var db = _dbFactory.OpenDbConnection())
//        {
//            if (ObjOperations == enmOperations.I)
//            {
//                 db.Insert(ObjUSR01);
//                response.message = "Inserted successfully";
//            }
//            else
//            {
//                db.Update(ObjUSR01);
//                response.message = "Updated successfully";
//            }
//        }

//        return response;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}
///// <summary>
///// Validates before delete
///// </summary>
///// <param name="id">User id to be deleted</param>
///// <returns>True if object is valid and false otherwise</returns>
//public Response ValidationDelete(int id)
//{
//    Response response = new Response();

//    if (!IsExist(id))
//    {
//        response.isError = true;
//        response.message = "User Not found";
//    }

//    return response;
//}

///// <summary>
///// Deactivates user
///// </summary>
///// <param name="id">Id of user to be deactivate</param>
///// <returns>Response indicating the result of the delete operation</returns>
//public Response Delete(int id)
//{
//    Response response = new Response();

//    try
//    {
//        using (var db = _dbFactory.OpenDbConnection())
//        {
//            USR01 user = db.SingleById<USR01>(id);

//            user.R01F05 = enmIsActive.N;

//            db.Update(user);
//        }

//        return response;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}
