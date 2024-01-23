using System.Collections.Generic;
using System.Linq;
using ORM.Models;
using ServiceStack.OrmLite;

namespace ORM.BusinessLogic
{
    /// <summary>
    /// Contains business logic for CLCompany controller
    /// </summary>
    public class BLCompany
    {
        /// <summary>
        /// Selects records from table CMP01
        /// </summary>
        /// <returns>All records of table CMP01</returns>
        public List<CMP01> Select()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {

                if (!db.TableExists<CMP01>())
                {
                    db.CreateTable<CMP01>();
                }
                List<CMP01> lstCMP01 = db.Select<CMP01>();
                return lstCMP01;
            }
        }

        /// <summary>
        /// Insert record into table CMP01
        /// </summary>
        /// <param name="objCMP01">new object of class CMP01 to be insert</param>
        /// <returns>Appropriate message according to request</returns>
        public string Insert(CMP01 objCMP01)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<CMP01>())
                {
                    db.CreateTable<CMP01>();
                }
                db.Insert(objCMP01);
                return "Success!";
            }
        }

        /// <summary>
        /// Updates record into table CMP01
        /// </summary>
        /// <param name="objCMP01">new object of class CMP01 to be edit</param>
        /// <returns>Appropriate message according to request</returns>
        public string Update(CMP01 objCMP01)
        {
            using(var db = BLConnection.dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<CMP01>())
                {
                    return "Table not exist";
                }
                var company = db.Select<CMP01>().FirstOrDefault(c => c.P01F01 == objCMP01.P01F01);
                if (company == null)
                {
                    return "Choose valid record to edit!";
                }
                db.Update<CMP01>(objCMP01, c => c.P01F01 == objCMP01.P01F01);
                return "Success!";
            }
        }

        /// <summary>
        /// Deletes row from table CMP01
        /// </summary>
        /// <param name="id">record id to be delete</param>
        /// <returns>Appropriate message according to request</returns>
        public string Delete(int id)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<CMP01>())
                {
                    return "Table not exist!";
                }
                var company = db.SingleById<CMP01>(id);
                if(company == null)
                {
                    return "Choose valid record to delete!";
                }
                db.DeleteById<CMP01>(id);
                return "Success!";
            }
        }
    }
}
