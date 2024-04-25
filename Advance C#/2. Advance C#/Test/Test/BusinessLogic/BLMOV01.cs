using System;
using System.Configuration;
using System.Data;
using ServiceStack.OrmLite;
using Test.Models;
using Test.Models.Enums;

namespace Test.BusinessLogic
{
    /// <summary>
    /// Business Logic class for MOV01 operations.
    /// </summary>
    public class BLMOV01
    {

        #region Public Properties

        /// <summary>
        /// Instance of MOV01 object.
        /// </summary>
        public MOV01 objMOV01 { get; set; }

        /// <summary>
        /// Instance of operation type enum.
        /// </summary>
        public enmOperations objOperation { get; set; }

        #endregion

        #region Public Members

        /// <summary>
        /// Connection string for the database.
        /// </summary>
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// Database Factory for ORM Lite.
        /// </summary>
        public OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        /// <summary>
        /// Instance of mapper class.
        /// </summary>
        public BLMapper objBLMapper = new BLMapper();

        /// <summary>
        /// Instance of database class for MOV01 model.
        /// </summary>
        public Database.DLMOV01 objDLMOV01 = new Database.DLMOV01();

        /// <summary>
        /// Instance of converter class.
        /// </summary>
        public BLConvertor objBLConvertor = new BLConvertor();

        #endregion

        #region Public Methods 

        /// <summary>
        /// Preprocessing before saving.
        /// </summary>
        /// <param name="objDTOMOV01">DTOMOV01 object to preprocess.</param>
        public void PreSave(DTOMOV01 objDTOMOV01)
        {
            objMOV01 = objBLMapper.Map<DTOMOV01, MOV01>(objDTOMOV01);
            objMOV01.V01F06 = DateTime.Now;
        }

        /// <summary>
        /// Checks weather movie exist or not
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        public bool IsExist(int id)
        {
            using (var db = dbFactory.OpenDbConnection())
            {
                var movie = db.SingleById<MOV01>(id);
                return movie == null ? false : true;
            }
        }

        /// <summary>
        /// Validates the object before saving or updating.
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not.</returns>
        public RES01 Validation()
        {
            RES01 response = new RES01();

            // Validation based on operation type
            if (objOperation == enmOperations.U)
            {
                if (!IsExist(objMOV01.V01F01))
                {
                    response.isError = true;
                    response.message = "Not Found";
                    return response;
                }
            }

            if (objOperation == enmOperations.I)
            {
                if (IsExist(objMOV01.V01F01))
                {
                    response.isError = true;
                    response.message = "Already exist";
                    return response;
                }
            }

            if (objMOV01.V01F04 > DateTime.Now)
            {
                response.isError = true;
                response.message = "Not released yet";
                return response;
            }

            return response;
        }

        /// <summary>
        /// Saves the MOV01 object.
        /// </summary>
        /// <returns>Response indicating the result of the save operation.</returns>
        public RES01 Save()
        {
            RES01 response = new RES01();

            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    if (objOperation == enmOperations.I)
                    {
                        db.Insert(objMOV01);
                        response.message = "Inserted successfully";
                    }
                    else
                    {
                        db.Update(objMOV01);
                        response.message = "Updated successfully";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validates object before deletion.
        /// </summary>
        /// <param name="id">The ID of the object to be deleted.</param>
        /// <returns>Validation response indicating if the object exists or not.</returns>
        public RES01 ValidationDelete(int id)
        {
            RES01 response = new RES01();

            if (!IsExist(id))
            {
                response.isError = true;
                response.message = "Not Found";
                return response;
            }

            return response;
        }

        /// <summary>
        /// Deletes MOV01 object by ID.
        /// </summary>
        /// <param name="id">The ID of the MOV01 object to delete.</param>
        /// <returns>Response indicating the result of the delete operation.</returns>
        public RES01 Delete(int id)
        {
            RES01 response = new RES01();

            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    db.DeleteById<MOV01>(id);
                    response.message = "Deleted successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves MOV01 object by ID.
        /// </summary>
        /// <param name="id">The ID of the MOV01 object to retrieve.</param>
        /// <returns>Response containing the retrieved MOV01 object.</returns>
        public RES01 GetById(int id)
        {
            RES01 response = new RES01();

            try
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    var movie = db.SingleById<MOV01>(id);
                    if (movie != null)
                    {
                        response.response = objBLConvertor.ToDataTable(movie);
                    }
                    else
                    {
                        response.isError = true;
                        response.message = "Not found";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves all MOV01 objects.
        /// </summary>
        /// <returns>Response containing all MOV01 objects.</returns>
        public RES01 GetAll()
        {
            RES01 response = new RES01();

            try
            {
                DataTable data = objDLMOV01.Select();

                if (data.Columns.Count > 0)
                {
                    response.response = data;
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

        #endregion

    }
}
