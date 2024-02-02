using System.Collections.Generic;
using System.Linq;
using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.BusinessLogic
{
    /// <summary>
    /// Handles business logic for album controller as well as implements IServies interface with type ALB01
    /// </summary>
    public class BLAlbum : IServices<ALB01>
    {
        /// <summary>
        /// List of albums
        /// </summary>
        public static List<ALB01> lstALB01 = new List<ALB01>();

        /// <summary>
        /// Handles request for get albums 
        /// </summary>
        /// <returns>List of albums</returns>
        public List<ALB01> GetElemets()
        {
            return lstALB01;
        }

        /// <summary>
        /// Handles request for getting one album's data
        /// </summary>
        /// <param name="id">id of album</param>
        /// <returns>object of class ALB01</returns>
        public ALB01 GetElementById(int id)
        {
            var objALB01 = lstALB01.FirstOrDefault(b => b.B01F01 == id);
            return objALB01;
        }

        /// <summary>
        /// Adds album into the list
        /// </summary>
        /// <param name="objALB01">object of class ALB01 which will be added</param>
        /// <returns>List of albums</returns>
        public List<ALB01> AddElement(ALB01 objALB01)
        {
            var objALB01Temp = lstALB01.FirstOrDefault(b => b.B01F01 == objALB01.B01F01);
            if (objALB01Temp == null)
            {
                lstALB01.Add(objALB01);
            }
            return lstALB01;
        }

        /// <summary>
        /// Removes album from list
        /// </summary>
        /// <param name="id">id of album</param>
        /// <returns>List of albums</returns>
        public List<ALB01> RemoveElement(int id)
        {
            lstALB01.RemoveAt(id);
            return lstALB01;
        }

        /// <summary>
        /// Updates album
        /// </summary>
        /// <param name="objALB01">object of class ALB01 which will be edited</param>
        /// <returns>List of albums</returns>
        public List<ALB01> UpdateElement(ALB01 objALB01)
        {
            var objALB01Temp = lstALB01.FirstOrDefault(b => b.B01F01 == objALB01.B01F01);
            if (objALB01Temp != null)
            {
                lstALB01[objALB01Temp.B01F01] = objALB01;
            }
            return lstALB01;
        }
    }
}