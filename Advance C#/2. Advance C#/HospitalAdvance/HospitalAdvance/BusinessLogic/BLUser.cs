using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using HospitalAdvance.DataBase;
using HospitalAdvance.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for user controller
    /// </summary>
    public class BLUser
    {

        #region Private Members

        /// <summary>
        /// Declares object of class BLDoctor
        /// </summary>
        private readonly BLDoctor _objBLDoctor;

        /// <summary>
        /// Declares object of class BLHelper
        /// </summary>
        private readonly BLHelper _objBLHelper;

        /// <summary>
        /// Declares object of class BLPatient
        /// </summary>
        private readonly BLPatient _objBLPatient;

        /// <summary>
        /// Declares object of class Cache
        /// </summary>
        private static readonly Cache _objCache = new Cache();

        /// <summary>
        /// Declares object of class DL
        /// </summary>
        private readonly DL _objDL;

        /// <summary>
        /// Declares Db factory instance
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Establishes connection and initializes objects
        /// </summary>
        public BLUser()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
            _objBLDoctor = new BLDoctor();
            _objBLHelper = new BLHelper();
            _objBLPatient = new BLPatient();
            _objDL = new DL();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs caching operations
        /// </summary>
        /// <param name="key">Key of cache</param>
        /// <param name="obj">Value</param>
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
        public List<object> GetCache()
        {
            List<object> cacheItems = new List<object>();

            // Get all cache keys
            var cacheKeys = _objCache.GetEnumerator();

            // Iterate through each cache item and add it to the list
            while (cacheKeys.MoveNext())
            {
                var key = cacheKeys.Key.ToString();
                var cacheItem = _objCache[key];

                // Create a Cache object and add it to the list
                cacheItems.Add(new { key, cacheItem });
            }

            return cacheItems;
        }

        /// <summary>
        /// Validates given object before presave
        /// </summary>
        /// <param name="model">Object of class USR02</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool preValidation(USR02 model)
        {
            if (model != null && model.ObjUSR01 != null && model.ObjRole != null)
            {
                return validateObjRole(model.ObjRole, model.ObjUSR01.R01F04);
            }

            return false;
        }


        public bool validateObjRole(dynamic obj, enmUserRole role)
        {
            var keySets = new Dictionary<enmUserRole, HashSet<string>>
            {
                [enmUserRole.Doctor] = new HashSet<string> { "F01F01", "F01F02", "F01F03", "F01F04", "F01F05", "F01F06" },
                [enmUserRole.Helper] = new HashSet<string> { "F02F01", "F02F02", "F02F03", "F02F04", "F02F05", "F02F06" },
                [enmUserRole.Patient] = new HashSet<string> { "N01F01", "N01F02", "N01F03", "N01F04", "N01F05", "N01F06" }
            };

            if (!keySets.ContainsKey(role))
            {
                return false; // Invalid role
            }

            var expectedKeys = keySets[role];

            // Check if all expected keys are present in the JObject
            var objProperties = ((JObject)obj).Properties();
            foreach (var key in expectedKeys)
            {
                if (!objProperties.Any(p => p.Name == key))
                {
                    return false;
                }
            }

            // Check if the number of keys matches the expected number
            return objProperties.Count() == expectedKeys.Count;
        }



        /// <summary>
        /// Prepares object as our need
        /// </summary>
        /// <param name="objUSR01">Object of class USR01</param>
        /// <returns>Prepared object</returns>
        public USR01 preSave(USR01 objUSR01)
        {
            objUSR01.R01F03 = BLSecurity.EncryptAes(objUSR01.R01F03, BLSecurity.key, BLSecurity.iv);

            return objUSR01;
        }

        /// <summary>
        /// Validates given object
        /// </summary>
        /// <param name="objUSR01">Object of class USR01</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool validation(USR01 objUSR01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var user = db.Select<USR01>().FirstOrDefault(x => x.R01F02 == objUSR01.R01F02);

                if (user != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Validates before delete
        /// </summary>
        /// <param name="id">User id to be deleted</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool validationDelete(int id)
        {
            var user = Select().Find(x => x.R01F01 == id);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert statement
        /// </summary>
        /// <param name="objUSR01">object of class USR01</param>
        /// <returns>Appropriate message</returns>
        public string Insert(USR01 objUSR01)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<USR01>())
                    {
                        db.CreateTable<USR01>();
                    }

                    db.Insert(objUSR01);

                    return "Success!";
                }
            }
            catch (Exception ex)  
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Inserts data to USR01 as well as into the other table according to user role
        /// </summary>
        /// <param name="model">Object of USR01 and object of second class</param>
        /// <returns>Appropriate message</returns>
        /// <exception cref="Exception"></exception>
        public string InsertData(USR02 model)
        {
            bool result = false;

            // Determine the user's role and insert them into the corresponding table
            switch (model.ObjUSR01.R01F04)
            {
                case enmUserRole.Doctor:

                    string jsonSTF01 = JsonConvert.SerializeObject(model.ObjRole);

                    STF01 objSTF01 = JsonConvert.DeserializeObject<STF01>(jsonSTF01);

                    objSTF01 = _objBLDoctor.preSave(objSTF01, model.ObjUSR01);

                    if (_objBLDoctor.validationInsert(objSTF01))
                    {
                        Insert(model.ObjUSR01);
                        _objBLDoctor.Insert(objSTF01);
                        result = true;
                    }

                    break;

                case enmUserRole.Helper:

                    string jsonSTF02 = JsonConvert.SerializeObject(model.ObjRole);

                    STF02 objSTF02 = JsonConvert.DeserializeObject<STF02>(jsonSTF02);

                    objSTF02 = _objBLHelper.preSave(objSTF02, model.ObjUSR01);

                    if (_objBLHelper.validationInsert(objSTF02))
                    {
                        Insert(model.ObjUSR01);
                        _objBLHelper.Insert(objSTF02);
                        result = true;
                    }

                    break;

                case enmUserRole.Patient:

                    string jsonPTN01 = JsonConvert.SerializeObject(model.ObjRole);

                    // Deserialize JSON to MyClass
                    PTN01 objPTN01 = JsonConvert.DeserializeObject<PTN01>(jsonPTN01);

                    objPTN01 = _objBLPatient.preSave(objPTN01, model.ObjUSR01);

                    if (_objBLPatient.validationInsert(objPTN01))
                    {
                        Insert(model.ObjUSR01);
                        _objBLPatient.Insert(objPTN01);
                        result = true;
                    }

                    break;

                default:
                    return "Unsupported user role.";

            }

            if (result)
                return "User added successfully to their respective role-based table.";
            else
                return "Invalid data";

        }

        /// <summary>
        /// Update statement
        /// </summary>
        /// <param name="objUSR01">object of class USR01</param>
        /// <returns>Appropriate message</returns>
        public string Update(USR01 objUSR01)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<USR01>())
                    {
                        db.CreateTable<CRG01>();
                        return "No records to be update!";
                    }

                    db.Update(objUSR01, u => u.R01F01 == objUSR01.R01F01);

                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Updates data to USR01 as well as into the other table according to user role
        /// </summary>
        /// <param name="model">Object of USR01 and object of second class</param>
        /// <returns>Appropriate message</returns>
        /// <exception cref="Exception"></exception>
        public string UpdateData(USR02 model)
        {
            bool result = false;

            // Determine the user's role and insert them into the corresponding table
            switch (model.ObjUSR01.R01F04)
            {
                case enmUserRole.Doctor:

                    string jsonSTF01 = JsonConvert.SerializeObject(model.ObjRole);

                    STF01 objSTF01 = JsonConvert.DeserializeObject<STF01>(jsonSTF01);

                    if (_objBLDoctor.validationUpdate(objSTF01))
                    {
                        _objBLDoctor.Update(objSTF01);
                        result = true;
                    }

                    break;

                case enmUserRole.Helper:

                    string jsonSTF02 = JsonConvert.SerializeObject(model.ObjRole);

                    STF02 objSTF02 = JsonConvert.DeserializeObject<STF02>(jsonSTF02);

                    if (_objBLHelper.validationUpdate(objSTF02))
                    {
                        _objBLHelper.Update(objSTF02);
                        result = true;
                    }

                    break;

                case enmUserRole.Patient:

                    string jsonPTN01 = JsonConvert.SerializeObject(model.ObjRole);

                    // Deserialize JSON to MyClass
                    PTN01 objPTN01 = JsonConvert.DeserializeObject<PTN01>(jsonPTN01);
                    if (_objBLPatient.validationUpdate(objPTN01))
                    {
                        _objBLPatient.Update(objPTN01);
                        result = true;
                    }

                    break;

                default:
                    return "Unsupported user role.";

            }

            if (result)
                return "User added successfully to their respective role-based table.";
            else
                return "Invalid data";
        }

        /// <summary>
        /// Deletes data to USR01 as well as from the other table according to user role
        /// </summary>
        /// <param name="model">Object of USR01 and object of second class</param>
        /// <returns>Appropriate message</returns>
        /// <exception cref="Exception"></exception>
        public string DeleteData(int id)
        {
            var user = Select().Find(x => x.R01F01 == id);

            bool result = false;

            // Determine the user's role and update them into the corresponding table
            switch (user.R01F04)
            {
                case enmUserRole.Doctor:

                    var objSTF01 = _objBLDoctor.Select().FirstOrDefault(x => x.F01F05 == id);

                    _objBLDoctor.Delete(objSTF01);

                    result = true;

                    break;

                case enmUserRole.Helper:

                    var objSTF02 = _objBLHelper.Select().FirstOrDefault(x => x.F02F05 == id);

                    _objBLHelper.Delete(objSTF02);

                    result = true;

                    break;

                case enmUserRole.Patient:

                    var objPTN01 = _objBLPatient.Select().FirstOrDefault(x => x.N01F05 == id);

                    _objBLPatient.Delete(objPTN01);

                    result = true;

                    break;

                default:
                    return "Unsupported user role.";
            }
            if (result)
                return "User deleted successfully to their respective role-based table.";
            else
                return "Invalid data";
        }

        /// <summary>
        /// Select statement
        /// </summary>
        /// <returns>List of users</returns>
        public List<USR01> Select()
        {
            try
            {
                var lstusr01 = _objDL.SelectUsers();
                return lstusr01;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Seperates username and password from request
        /// </summary>
        /// <param name="Request">Current request</param>
        /// <returns>Username and password</returns>
        public string[] GetUsernamePassword(HttpRequestMessage Request)
        {
            string authToken = Request.Headers.Authorization.Parameter;
            byte[] authBytes = Convert.FromBase64String(authToken);
            authToken = Encoding.UTF8.GetString(authBytes);
            string[] usernamepassword = authToken.Split(':');
            return usernamepassword;
        }

        /// <summary>
        /// Gets user detail from JWT token
        /// </summary>
        /// <param name="actionContext">Current context</param>
        /// <returns>User</returns>
        public USR01 GetUser(HttpActionContext actionContext)
        {
            string tokenValue = actionContext.Request.Headers.Authorization.Scheme;

            string jwtEncodedPayload = tokenValue.Split('.')[1];

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

            USR01 user = Select().FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());

            return user;

        }

        #endregion

    }
}