using System.Collections.Generic;
using System.Linq;
using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.BusinessLogic
{
    public class BLAlbum : IServices<ALB01>
    {
        public static List<ALB01> lstALB01 = new List<ALB01>();
        public List<ALB01> GetElemets()
        {
            return lstALB01;
        }

        public ALB01 GetElementById(int id)
        {
            var objALB01 = lstALB01.FirstOrDefault(b => b.B01F01 == id);
            return objALB01;
        }
        public List<ALB01> AddElement(ALB01 objALB01)
        {
            var objALB01Temp = lstALB01.FirstOrDefault(b => b.B01F01 == objALB01.B01F01);
            if (objALB01Temp == null)
            {
                lstALB01.Add(objALB01);
            }
            return lstALB01;
        }
        public List<ALB01> RemoveElement(int id)
        {
            lstALB01.RemoveAt(id);
            return lstALB01;
        }
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