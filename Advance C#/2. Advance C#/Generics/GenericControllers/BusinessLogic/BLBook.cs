using System.Collections.Generic;
using System.Linq;
using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.BusinessLogic
{
    /// <summary>
    /// Handles business logic for book controller as well as implements IServies interface with type BOK01
    /// </summary>
    public class BLBook : IServices<BOK01>
    {
        /// <summary>
        /// List of books
        /// </summary>
        public static List<BOK01>  lstBOK01 = new List<BOK01>();

        /// <summary>
        /// Handles request for get books 
        /// </summary>
        /// <returns>List of books</returns>
        public List<BOK01> GetElemets() {
            return lstBOK01;
        }

        /// <summary>
        /// Handles request for getting one book's data
        /// </summary>
        /// <param name="id">id of book</param>
        /// <returns>object of class BOK01</returns>
        public BOK01 GetElementById(int id)
        {
            var objBOK01 = lstBOK01.FirstOrDefault(b => b.K01F01 == id);
            return objBOK01;
        }

        /// <summary>
        /// Adds book into the list
        /// </summary>
        /// <param name="objBOK01">object of class BOk01 which will be added</param>
        /// <returns>List of books</returns>
        public List<BOK01> AddElement(BOK01 objBOK01)
        {
            var objBOK01Temp = lstBOK01.FirstOrDefault(b => b.K01F01 == objBOK01.K01F01);
            if(objBOK01Temp == null)
            {
                lstBOK01.Add(objBOK01);
            }
            return lstBOK01;
        }

        /// <summary>
        /// Removes book from list
        /// </summary>
        /// <param name="id">id of book</param>
        /// <returns>List of books</returns>
        public List<BOK01> RemoveElement(int id)
        {
            lstBOK01.RemoveAt(id);
            return lstBOK01;
        }

        /// <summary>
        /// Updates book
        /// </summary>
        /// <param name="objBOK01">object of class BOk01 which will be edited</param>
        /// <returns>List of books</returns>
        public List<BOK01> UpdateElement(BOK01 objBOK01)
        {
            var objBOK01Temp = lstBOK01.FirstOrDefault(b => b.K01F01 == objBOK01.K01F01);
            if (objBOK01Temp != null)
            {
                lstBOK01[objBOK01Temp.K01F01] = objBOK01;
            }
            return lstBOK01;
        }
    }
}