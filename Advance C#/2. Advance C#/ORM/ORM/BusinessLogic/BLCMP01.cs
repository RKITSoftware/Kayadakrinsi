using System.Collections.Generic;
using System.Diagnostics;
using ORM.Models;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;

namespace ORM.BusinessLogic
{
    /// <summary>
    /// Contains business logic for CLCompany controller.
    /// </summary>
    public class BLCMP01
    {
        /// <summary>
        /// Declares object of class RES01
        /// </summary>
        public RES01 ObjRES01 { get; set; } = new RES01();

        /// <summary>
        /// Declares object of class CMP01 
        /// </summary>
        public CMP01 ObjCMP01 { get; set; }

        /// <summary>
        /// Declares a list of CMP01 objects.
        /// </summary>
        public List<CMP01> LstCMP01 { get; set; }

        /// <summary>
        /// Declares object of class BLMapper
        /// </summary>
        public BLMapper<DTOCMP01, CMP01> objBLMapper = new BLMapper<DTOCMP01, CMP01>();

        /// <summary>
        ///  Declares object of class BLConvertor
        /// </summary>
        public BLConvertor objBLConvertor = new BLConvertor();

        /// <summary>
        /// Preprocessing before saving.
        /// </summary>
        /// <param name="objDTOCMP01">DTOCMP01 object to preprocess.</param>
        public void PreSave(DTOCMP01 objDTOCMP01)
        {
            ObjCMP01 = objBLMapper.Map(objDTOCMP01);
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<CMP01>();
            }
        }

        /// <summary>
        /// Preprocessing before saving.
        /// </summary>
        /// <param name="LstDTOCMP01">LstDTOCMP01 is list of objects of DTOCMP01 to preprocess.</param>
        public void PreSave(List<DTOCMP01> LstDTOCMP01)
        {
            LstCMP01 = new List<CMP01>();
            foreach(var objDTOCMP01 in LstDTOCMP01)
            {
                LstCMP01.Add(objBLMapper.Map(objDTOCMP01));
            }
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<CMP01>();
            }
        }

        /// <summary>
        /// Selects all records from table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 Select()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists(typeof(CMP01));
                db.ChangeDatabase("movies");
                LstCMP01 = db.Select<CMP01>();
                ObjRES01.isError = false;
                ObjRES01.message = "Success";
                ObjRES01.response = objBLConvertor.ToDataTable(LstCMP01);

                return ObjRES01;
            }
        }

        /// <summary>
        /// Selects records from table CMP01 based on provided IDs.
        /// </summary>
        /// <param name="lstIDs">List of IDs to select records.</param>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 SelectByIDs(List<int> lstIDs)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<CMP01>();
                LstCMP01 = db.SelectByIds<CMP01>(lstIDs);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";
                ObjRES01.response = objBLConvertor.ToDataTable(LstCMP01);

                return ObjRES01;
            }
        }

        /// <summary>
        /// Selects a single record from table CMP01 based on provided ID.
        /// </summary>
        /// <param name="id">ID of the record to retrieve.</param>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 SingleById(int id)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<CMP01>();
                ObjCMP01 = db.SingleById<CMP01>(id);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";
                ObjRES01.response = objBLConvertor.ToDataTable<CMP01>(ObjCMP01);

                return ObjRES01;
            }
        }

        /// <summary>
        /// Inserts only specified fields into table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 InsertOnly()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists(typeof(CMP01));
                db.InsertOnly<CMP01>(new CMP01 { P01F02 = "RKIT"}, c => c.P01F02);
                
                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Inserts a record into table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 Insert()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                
                db.Insert(ObjCMP01);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Inserts multiple records into table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 InsertAll()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<CMP01>();
                db.InsertAll<CMP01>(LstCMP01);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Updates specified fields of records in table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 UpdateOnly()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.UpdateOnly<CMP01>(() => new CMP01 { P01F04 = "Production" }, where: p => p.P01F04 != "IT" );

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Updates a record in table CMP01 based on the provided CMP01 object.
        /// </summary>
        /// <param name="id">Id of CMP01 object to be update</param>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 Update(int id)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                ObjCMP01.P01F01 = id;
                db.Update<CMP01>(ObjCMP01, c => c.P01F01 == id);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Updates multiple records in table CMP01.
        /// </summary>
        /// <param name="lstIDs">List of id of CMP01 object to be update</param>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 UpdateAll()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                //SqlExpression<CMP01> query = db.From<CMP01>()
                //                            .Where(x => Sql.In(x.P01F01, 3,4));

                //LstCMP01 = db.SqlList<CMP01>(query);

                db.UpdateAll<CMP01>(new[] {new CMP01 { P01F02="AQua",P01F03="Rajkot",P01F04="Production",P01F05=300}, new CMP01 { P01F02 = "AQua", P01F03 = "Rajkot", P01F04 = "Production", P01F05 = 300 } });

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Updates only specified fields of records in table CMP01 based on the provided query.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 UpdateOnlyFields()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                SqlExpression<CMP01> query = db.From<CMP01>()
                                             .Where(c => c.P01F05 != 0)
                                             .Update(c => c.P01F02);
                db.CreateTableIfNotExists(typeof(CMP01));
                db.UpdateOnlyFields<CMP01>(new CMP01 { P01F02 = "Shree", P01F05 = 289 }, query);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Deletes a row from table CMP01 based on the provided record ID.
        /// </summary>
        /// <param name="id">Record ID to be deleted.</param>
        /// <returns>Appropriate message according to request.</returns>
        public RES01 Delete(int id)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.Delete<CMP01>(c => c.P01F01 == id);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Deletes a row from table CMP01 based on the provided record ID.
        /// </summary>
        /// <param name="id">Record ID to be deleted.</param>
        /// <returns>Appropriate message according to request.</returns>
        public RES01 DeleteById(int id)
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                db.DeleteById<CMP01>(id);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Deletes all rows from table CMP01 where the P01F02 field is equal to "RKIT".
        /// </summary>
        /// <returns>Appropriate message according to request.</returns>
        public RES01 DeleteAll()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                IEnumerable<CMP01> LstCMP01 = db.Select<CMP01>();

                db.DeleteAll(LstCMP01);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";

                return ObjRES01;
            }
        }

        /// <summary>
        /// Retrieves schema tables.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 GetSchemaTables()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                Dictionary<string, List<string>> result = db.GetSchemaTables();

                Debug.WriteLine(result);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";
               
                return ObjRES01;
            }
        }

        /// <summary>
        /// Executes a scalar query on table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 Scalar()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                var q = db.From<CMP01>()
                    .Where(a => a.P01F01 == 11)
                    .Select(x => new { x.P01F01, x.P01F02, x.P01F03, x.P01F04, x.P01F05 });

                var result = db.Scalar<int>(q);

                Debug.WriteLine(result);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";
               
                return ObjRES01;
            }
        }

        /// <summary>
        /// Executes a group by query on table CMP01.
        /// </summary>
        /// <returns>RES01 object containing the result of the operation.</returns>
        public RES01 GroupBy()
        {
            using (var db = BLConnection.dbFactory.OpenDbConnection())
            {
                SqlExpression<CMP01> query = db.From<CMP01>()
                    .GroupBy(x => x.P01F02)
                    .Select(x => new { x.P01F01, x.P01F02, x.P01F03, x.P01F04, x.P01F05 })
                    .Where(x => Sql.In(x.P01F05, 0, 200, 500))
                    .OrderByDescending(x => x.P01F01);

                var resultList = db.SqlList<CMP01>(query);
                List<CMP01> resultColumn = db.SqlColumn<CMP01>(query);
                HashSet<int> resultColumnDistinct = db.ColumnDistinct<int>(query);

                var result = new { SqlList = resultList, SqlColumn = resultColumn, ColumnDistinct = resultColumnDistinct };
                
                Debug.WriteLine(result);

                ObjRES01.isError = false;
                ObjRES01.message = "Success";
               
                return ObjRES01;
            }
        }
    }
}


