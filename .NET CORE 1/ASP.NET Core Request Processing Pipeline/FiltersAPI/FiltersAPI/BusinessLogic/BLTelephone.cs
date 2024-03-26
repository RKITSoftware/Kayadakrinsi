using FiltersAPI.Models;

namespace FiltersAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for telephone controller actions
    /// </summary>
    public class BLTelephone
    {

        #region Public Members

        /// <summary>
        /// Next id tracker
        /// </summary>
        public static int idCount = 0;

        /// <summary>
        /// List of telephone record
        /// </summary>
        public static List<TEL01> lstTEL01 = new List<TEL01>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates object to be added to the list of telephone records
        /// </summary>
        /// <param name="objTEL01">Object of class TEL01</param>
        /// <returns>True if object is valid, false otherwise</returns>
        public bool Validation(TEL01 objTEL01)
        {
            try
            {
                var record = lstTEL01.FirstOrDefault(t => t.L01F04 == objTEL01.L01F04);

                if (record != null)
                {
                    return false;
                }
                else if (objTEL01.L01F03 < 1000 && objTEL01.L01F03 > 0 &&
                        objTEL01.L01F04 < 10000000000 && objTEL01.L01F04 > 1000000000)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validates object to be update into the list of telephone records
        /// </summary>
        /// <param name="objTEL01">Object of class TEL01</param>
        /// <returns>True if object is valid, false otherwise</returns>
        public bool validationUpdate(TEL01 objTEL01)
        {
            try
            {
                var record = lstTEL01.FirstOrDefault(t => t.L01F01 == objTEL01.L01F01);

                if (record == null)
                {
                    return false;
                }
                else if (objTEL01.L01F03 < 1000 && objTEL01.L01F03 > 0 &&
                        objTEL01.L01F04 < 10000000000 && objTEL01.L01F04 > 1000000000)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets single telephone record by id
        /// </summary>
        /// <param name="id">Id of record user wants to get</param>
        /// <returns>Object of class TEL01 if exist, null otherwise</returns>
        public TEL01? GetRecordById(int id)
        {
            try
            {
                TEL01 record = lstTEL01.FirstOrDefault(r => r.L01F01 == id);
                if (record != null)
                {
                    return record;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adds telephone record into the list of telephone record
        /// </summary>
        /// <param name="objTEL01">Object of class TEL01</param>
        /// <returns>Appropriate message</returns>
        public string AddRecord(TEL01 objTEL01)
        {
            idCount++;
            objTEL01.L01F01 = idCount;

            lstTEL01.Add(objTEL01);

            return "Record added successfully.";
        }

        /// <summary>
        /// Updates telephone record into the list of telephone record
        /// </summary>
        /// <param name="objTEL01">Object of class TEL01</param>
        /// <returns>Appropriate message</returns>
        public string UpdateRecord(TEL01 objTEL01)
        {
            var record = lstTEL01.FirstOrDefault(r => r.L01F01 == objTEL01.L01F01);

            lstTEL01[record.L01F01 - 1] = objTEL01;

            return "Record updated successfully.";
        }

        /// <summary>
        /// Deletes telephone record from the list of telephone record
        /// </summary>
        /// <param name="id">Id of record user wants to delete</param>
        /// <returns>Appropriate message, null otherwise</returns>
        public string? DeleteRecord(int id)
        {
            var record = lstTEL01.FirstOrDefault(r => r.L01F01 == id);

            if (record != null)
            {
                lstTEL01.Remove(record);
                return "Record deleted successfully.";
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
