using CRUD_API.DataBase;
using CRUD_API.Models;

namespace CRUD_API.BusinessLogic
{
    /// <summary>
    /// Contains logic for order operations
    /// </summary>
    public class BLORD01
    {

        #region Public Members

        /// <summary>
        /// Declares Object of class RES01
        /// </summary>
        public RES01 ObjRES01 = new RES01();

        /// <summary>
        /// Declares Object of class ORD01
        /// </summary>
        public ORD01 ObjORD01 = new ORD01();

        /// <summary>
        /// Declares Object of class BLConvertor
        /// </summary>
        public BLConvertor objBLConvertor = new BLConvertor();

        /// <summary>
        /// Declares Object of class DL
        /// </summary>
        public DL ObjDL = new DL();

        /// <summary>
        /// Declares Object of class BLMapper
        /// </summary>
        public BLMapper<DTOORD01, ORD01> objBLMapper = new BLMapper<DTOORD01, ORD01>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Presorms neccesary actions before saving
        /// </summary>
        /// <param name="objORD01">Object of class ORD01</param>
        public void PreSave(DTOORD01 objDTOORD01)
        {
            ObjORD01 = objBLMapper.Map(objDTOORD01);
        }

        /// <summary>
        /// Validates given object
        /// </summary>
        /// <returns>True if object is valid, false otherwise</returns>
        public RES01 Validation()
        {
            if (!ObjDL.TableExists(typeof(ORD01)))
            {
                ObjDL.CreateTable();
            }
            if (ObjORD01.D01F03 > 0 && ObjORD01.D01F04 > 0)
            {
                ObjRES01.isError = false;
                ObjRES01.message = "Ok";

                return ObjRES01;
            }
            ObjRES01.isError = true;
            ObjRES01.message = "Invalid data";

            return ObjRES01;
        }


        /// <summary>
        /// Insert statement
        /// </summary>
        /// <param name="ObjORD01">object of class ORD01</param>
        public RES01 Insert()
        {
            string result = ObjDL.Insert(ObjORD01);
            if (result == "Ok")
            {
                ObjRES01.isError = false;
                ObjRES01.message = result;
                return ObjRES01;
            }
            ObjRES01.isError = true;
            ObjRES01.message = result;
            return ObjRES01;
        }

        /// <summary>
        /// Update statement
        /// </summary>
        public RES01 Update(int id)
        {
            string result = ObjDL.Update(id, ObjORD01);
            if (result == "Ok")
            {
                ObjRES01.isError = false;
                ObjRES01.message = result;
                return ObjRES01;
            }
            ObjRES01.isError = true;
            ObjRES01.message = result;
            return ObjRES01;
        }

        /// <summary>
        /// Delete statement
        /// </summary>
        public RES01 Delete(int id)
        {
            if (!ObjDL.TableExists(typeof(ORD01)))
            {
                ObjDL.CreateTable();
            }
            string result = ObjDL.Delete(id);
            if (result == "Ok")
            {
                ObjRES01.isError = false;
                ObjRES01.message = result;
                return ObjRES01;
            }
            ObjRES01.isError = true;
            ObjRES01.message = result;
            return ObjRES01;
        }

        /// <summary>
        /// Select statement
        /// </summary>
        public RES01 Select()
        {
            if (!ObjDL.TableExists(typeof(ORD01)))
            {
                ObjDL.CreateTable();
            }
            int result = ObjDL.Select().Count;
            if (result >= 0)
            {
                ObjRES01.isError = false;
                ObjRES01.message = "Ok";
                ObjRES01.response = objBLConvertor.ToDataTable(ObjDL.Select());
                return ObjRES01;
            }
            ObjRES01.isError = true;
            ObjRES01.message = "Error";
            return ObjRES01;
        }

        /// <summary>
        /// Drop statement
        /// </summary>
        public RES01 Drop()
        {
            if (!ObjDL.TableExists(typeof(ORD01)))
            {
                ObjDL.CreateTable();
            }
            string result = ObjDL.Drop();
            if (result == "Ok")
            {
                ObjRES01.isError = false;
                ObjRES01.message = result;
                return ObjRES01;
            }
            ObjRES01.isError = true;
            ObjRES01.message = result;
            return ObjRES01;
        }

        #endregion

    }
}