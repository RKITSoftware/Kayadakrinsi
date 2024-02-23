using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Caching;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
	/// <summary>
	/// For taking input model
	/// </summary>
	public class UserAddModel
	{
		/// <summary>
		/// Object of USR01
		/// </summary>
		public USR01 ObjUSR01 { get; set; }

		/// <summary>
		/// Object of either STF01, STF02 or PTN01
		/// </summary>
		public dynamic ObjRole { get; set; }
	}

	/// <summary>
	/// Handles logic for user controller
	/// </summary>
	public class BLUser 
	{
		#region Private Members

		/// <summary>
		/// Connection string
		/// </summary>
		private static readonly string _connectionString;

		/// <summary>
		/// Connection object of class MySqlConnection
		/// </summary>
		private static readonly MySqlConnection _connection;

		/// <summary>
		/// Declares object of class BLDoctor
		/// </summary>
		private static readonly BLDoctor _objSTF01;

		/// <summary>
		/// Declares object of class BLHelper
		/// </summary>
		private static readonly BLHelper _objSTF02;

		/// <summary>
		/// Declares object of class BLPatient
		/// </summary>
		private static readonly BLPatient _objPTN01;

		/// <summary>
		/// Declares object of class Cache
		/// </summary>
		private static readonly Cache _objCache;

		/// <summary>
		/// Declares Db factory instance
		/// </summary>
		private readonly static IDbConnectionFactory _dbFactory;

		#endregion

		#region Constructors

		/// <summary>
		/// Establishes connection and initializes objects
		/// </summary>
		static BLUser()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			_connection = new MySqlConnection(_connectionString);
			_dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
			_objSTF01 = new BLDoctor();
			_objSTF02 = new BLHelper();
			_objPTN01 = new BLPatient();
			_objCache = new Cache();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// open connection to database
		/// </summary>
		/// <returns>true if connection opened else false</returns>
		private static bool OpenConnection()
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
		private static bool CloseConnection()
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
		/// Performs caching operations
		/// </summary>
		/// <param name="key">Key of cache</param>
		/// <param name="obj">Value</param>
		public static void CacheOperations(string key,object obj)
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
		/// Creates table statement
		/// </summary>
		/// <returns>Appropriate message</returns>
		public static string CreateTable<T>() where T : class
		{
			using (var db = _dbFactory.OpenDbConnection())
			{

				if (!db.TableExists<T>())
				{
					db.CreateTable<T>();
				}

				return "Table created successfully";
			}
		}

		/// <summary>
		/// Checks weather table exist or not in MySqlConnection
		/// </summary>
		/// <returns>True if table exists else false</returns>
		public static bool TableExists(Type type)
		{
			try
			{
				if (OpenConnection() == true)
				{
					MySqlCommand command = new MySqlCommand($"SHOW TABLES LIKE '@type'", _connection);

					//Execute command
					var result = command.ExecuteScalar();
					if (result != null)
					{
						CloseConnection();
						return true;
					}
					CloseConnection();
				}
				return false;
			}
			catch
			{
				CloseConnection();
				return false;
			}
		}

		/// <summary>
		/// Insert statement
		/// </summary>
		/// <param name="objUSR01">object of class USR01</param>
		/// <returns>Appropriate message</returns>
		public string Insert(USR01 objUSR01)
		{
			objUSR01.R01F03 = BLSecurity.EncryptAes(objUSR01.R01F03, BLSecurity.key, BLSecurity.iv);
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<USR01>())
				{
					db.CreateTable<USR01>();
				}
				db.Insert<USR01>(objUSR01);
				}
				return "Success!";
			}

		/// <summary>
		/// Inserts data to USR01 as well as into the other table according to user role
		/// </summary>
		/// <param name="model">Object of USR01 and object of second class</param>
		/// <returns>Appropriate message</returns>
		/// <exception cref="Exception"></exception>
		public string InsertData(UserAddModel model)
		{
			if (model == null || model.ObjUSR01 == null || model.ObjRole == null)
			{
				return "Invalid input data.";
			}

			// Insert the user into the USR01 table
			string result = Insert(model.ObjUSR01);

			if (result != "Success!")
			{
				throw new Exception(result);
			}

			// Determine the user's role and insert them into the corresponding table
			switch (model.ObjUSR01.R01F04)
			{
				case enmUserRole.Doctor:
					string json = JsonConvert.SerializeObject(model.ObjRole);

					// Deserialize JSON to MyClass
					STF01 obj = JsonConvert.DeserializeObject<STF01>(json);
					if (obj == null)
					{
						return "ObjRole is not of type STF01.";
					}
					result = _objSTF01.Insert(obj);
					break;
				case enmUserRole.Helper:
					string json2 = JsonConvert.SerializeObject(model.ObjRole);

					// Deserialize JSON to MyClass
					STF02 obj2 = JsonConvert.DeserializeObject<STF02>(json2);
					if (obj2 == null)
					{
						return "ObjRole is not of type STF02.";
					}
					result = _objSTF02.Insert(obj2);
					break;
				case enmUserRole.Patient:
					string json3 = JsonConvert.SerializeObject(model.ObjRole);

					// Deserialize JSON to MyClass
					PTN01 obj3 = JsonConvert.DeserializeObject<PTN01>(json3);
					if (obj3 == null)
					{
						return "ObjRole is not of type PTN01.";
					}
					result = _objPTN01.Insert(obj3);
					break;
				default:
					// Handle unsupported roles
					return "Unsupported user role.";
			}

			if (result != "Success!")
			{
				throw new Exception(result);
			}

			return "User added successfully to their respective role-based table.";
		}

		/// <summary>
		/// Update statement
		/// </summary>
		/// <param name="objUSR01">object of class USR01</param>
		/// <returns>Appropriate message</returns>
		public string Update(USR01 objUSR01)
		{
			objUSR01.R01F03 = BLSecurity.EncryptAes(objUSR01.R01F03, BLSecurity.key, BLSecurity.iv);
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<USR01>())
				{
					return "Table not exist";
				}
				var user = db.Select<USR01>().FirstOrDefault(u => u.R01F01 == objUSR01.R01F01);
				if (user == null)
				{
					return "Choose valid record to edit!";
				}
				db.Update(objUSR01, u => u.R01F01 == objUSR01.R01F01);
				return "Success!";
			}
		}

		/// <summary>
		/// Updates data to USR01 as well as into the other table according to user role
		/// </summary>
		/// <param name="model">Object of USR01 and object of second class</param>
		/// <returns>Appropriate message</returns>
		/// <exception cref="Exception"></exception>
		public string UpdateData(UserAddModel model)
		{
			if (model == null || model.ObjUSR01 == null || model.ObjRole == null)
			{
				return "Invalid input data.";
			}

			// Insert the user into the USR01 table
			string result = Update(model.ObjUSR01);

			if (result != "Success!")
			{
				throw new Exception(result);
			}

			// Determine the user's role and insert them into the corresponding table
			switch (model.ObjUSR01.R01F04)
			{
				case enmUserRole.Doctor:
					string json = JsonConvert.SerializeObject(model.ObjRole);

					// Deserialize JSON to MyClass
					STF01 obj = JsonConvert.DeserializeObject<STF01>(json);
					if (obj == null)
					{
						return "ObjRole is not of type STF01.";
					}
					result = _objSTF01.Update(obj);
					break;
				case enmUserRole.Helper:
					string json2 = JsonConvert.SerializeObject(model.ObjRole);

					// Deserialize JSON to MyClass
					STF02 obj2 = JsonConvert.DeserializeObject<STF02>(json2);
					if (obj2 == null)
					{
						return "ObjRole is not of type STF02.";
					}
					result = _objSTF02.Update(obj2);
					break;
				case enmUserRole.Patient:
					string json3 = JsonConvert.SerializeObject(model.ObjRole);

					// Deserialize JSON to MyClass
					PTN01 obj3 = JsonConvert.DeserializeObject<PTN01>(json3);
					if (obj3 == null)
					{
						return "ObjRole is not of type PTN01.";
					}
					result = _objPTN01.Update(obj3);
					break;
				default:
					// Handle unsupported roles
					return "Unsupported user role.";
			}

			if (result != "Success!")
			{
				throw new Exception(result);
			}

			return "User updated successfully to their respective role-based table.";
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
			if (user == null) 
			{
				return "Invalid id";
			}
			string result;
			// Determine the user's role and insert them into the corresponding table
			switch (user.R01F04)
			{
				case enmUserRole.Doctor:
					var obj = _objSTF01.Select().FirstOrDefault(x => x.F01F05 == id);
					if(obj == null)
					{
						return "Not found doctor with this id";
					}
					obj.F01F06 = false;
					result = _objSTF01.Update(obj);
					break;
				case enmUserRole.Helper:
					var obj2 = _objSTF02.Select().FirstOrDefault(x => x.F02F05 == id);
					if (obj2 == null)
					{
						return "Not found helper with this id";
					}
					obj2.F02F06 = false;
					result = _objSTF02.Update(obj2);
					break;
				case enmUserRole.Patient:
					var obj3 = _objPTN01.Select().FirstOrDefault(x => x.N01F05 == id);
					if (obj3 == null)
					{
						return "Not found patiebt with this id";
					}
					obj3.N01F06 = false;
					result = _objPTN01.Update(obj3);
					break;
				default:
					// Handle unsupported roles
					return "Unsupported user role.";
			}
			if (result != "Success!")
			{
				throw new Exception(result);
			}
			return "User deleted successfully to their respective role-based table.";
		}

		/// <summary>
		/// Select statement
		/// </summary>
		/// <returns>List of users</returns>
		public static List<USR01> Select()
		{
			if (!TableExists((typeof(USR01))))
			{
				CreateTable<USR01>();
			}
			string query = "SELECT * FROM USR01";

			List<USR01> lstusr01 = new List<USR01>();
			//Open connection
			if (OpenConnection() == true)
			{
				//Create Command
				MySqlCommand command = new MySqlCommand(query, _connection);
				//Create a data reader and Execute the command
				MySqlDataReader dataReader = command.ExecuteReader();

				//Read the data and store them in the list
				while (dataReader.Read())
				{
					var objUSR01 = new USR01();
					objUSR01.R01F01 = (int)dataReader[0];
					objUSR01.R01F02 = (string)dataReader[1];
					var password = BLSecurity.DecryptAes((string)dataReader[2], BLSecurity.key, BLSecurity.iv);
					objUSR01.R01F03 = password;
					objUSR01.R01F04 = (enmUserRole)dataReader[3];
					lstusr01.Add(objUSR01);
				}

				//close Data Reader
				dataReader.Close();

				CacheOperations("Users", lstusr01);

				//close Connection
				CloseConnection();
			}
			return lstusr01;
		}

		/// <summary>
		/// Seperates username and password from request
		/// </summary>
		/// <param name="Request">Current request</param>
		/// <returns>Username and password</returns>
		public static string[] GetUsernamePassword(HttpRequestMessage Request)
		{
			string authToken = Request.Headers.Authorization.Parameter;
			byte[] authBytes = Convert.FromBase64String(authToken);
			authToken = Encoding.UTF8.GetString(authBytes);
			string[] usernamepassword = authToken.Split(':');
			return usernamepassword;
		}

		#endregion
	}
}