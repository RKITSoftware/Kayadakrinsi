using System.Collections.Generic;
using System.Linq;
using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.BusinessLogic
{
    public class BLBook : IServices<BOK01>
    {
        public static List<BOK01>  lstBOK01 = new List<BOK01>();
        public List<BOK01> GetElemets() {
            return lstBOK01;
        }

        public BOK01 GetElementById(int id)
        {
            var objBOK01 = lstBOK01.FirstOrDefault(b => b.K01F01 == id);
            return objBOK01;
        }
        public List<BOK01> AddElement(BOK01 objBOK01)
        {
            var objBOK01Temp = lstBOK01.FirstOrDefault(b => b.K01F01 == objBOK01.K01F01);
            if(objBOK01Temp == null)
            {
                lstBOK01.Add(objBOK01);
            }
            return lstBOK01;
        }
        public List<BOK01> RemoveElement(int id)
        {
            lstBOK01.RemoveAt(id);
            return lstBOK01;
        }
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