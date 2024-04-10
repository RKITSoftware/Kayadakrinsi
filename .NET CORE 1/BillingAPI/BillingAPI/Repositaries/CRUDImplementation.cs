using BillingAPI.BusinessLogic;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;
using ServiceStack.OrmLite;

namespace BillingAPI.Repositaries
{
    public class CRUDImplementation<T> : ICRUD where T : class
    {
        public static BLConvertor objBLConvertor = new BLConvertor();

        /// <summary>
        /// Db factory
        /// </summary>
        private readonly OrmLiteConnectionFactory dbFactory;

        public static string connectionString;

        public T obj { get; set; }

        public RES01 objRES01 = new RES01();

        public CRUDImplementation()
        {
            connectionString = GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            // Replace "DefaultConnection" with the key used in appsettings.json for your connection string
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }

        public RES01 CreateTable()
        {
            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    if (db.TableExists<T>())
                    {
                        db.CreateTable<T>();
                        objRES01.isError = false;
                        objRES01.message = String.Format(@"Table {0} created successfully.", typeof(T).Name);
                    }
                    else
                    {
                        objRES01.isError = true;
                        objRES01.message = "Table exist already";
                    }
                }
                return objRES01;
            }
            catch (Exception ex)
            {
                objRES01.isError = true;
                objRES01.message = ex.Message;
                return objRES01;
            }
        }

        public RES01 Select()
        {
            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    List<T> data = db.Select<T>();

                    objBLConvertor.ConvertUnsupportedTypes<T>(data);

                    objRES01.isError = false;
                    objRES01.message = "Success";
                    objRES01.response = objBLConvertor.ListToDataTable<T>(data);
                }
                return objRES01;
            }
            catch (Exception ex)
            {
                objRES01.isError = true;
                objRES01.message = ex.Message;
                return objRES01;
            }
        }

        public RES01 Add()
        {
            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<USR01>())
                    {
                        db.CreateTable<USR01>();
                    }
                    db.Insert<T>(obj);

                    objRES01.isError = false;
                    objRES01.message = "Success";
                }
                return objRES01;
            }
            catch (Exception ex)
            {
                objRES01.isError = true;
                objRES01.message = ex.Message;
                return objRES01;
            }
        }

        public RES01 Update()
        {
            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    db.Update<T>(obj);

                    objRES01.isError = false;
                    objRES01.message = "Success";
                }
                return objRES01;
            }
            catch (Exception ex)
            {
                objRES01.isError = true;
                objRES01.message = ex.Message;
                return objRES01;
            }
        }

        public RES01 Delete(int id)
        {
            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    db.DeleteById<T>(id);

                    objRES01.isError = false;
                    objRES01.message = "Success";
                }
                return objRES01;
            }
            catch (Exception ex)
            {
                objRES01.isError = true;
                objRES01.message = ex.Message;
                return objRES01;
            }
        }

    }
}
