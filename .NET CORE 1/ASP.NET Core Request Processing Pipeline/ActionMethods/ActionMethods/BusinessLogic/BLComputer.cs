using ActionMethods.Models;

namespace ActionMethods.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLAction controller
    /// </summary>
    public class BLComputer
    {
        /// <summary>
        /// List of computers
        /// </summary>
        public static List<COM01> lstCOM01 = new List<COM01>
        {
            new COM01{ M01F01 = Guid.NewGuid(), M01F02 = "HP Elite 8300 PC", M01F03 = "HP", M01F04 = "3rd generation Intel core i7", M01F05 = 32, M01F06 = 45000 },
            new COM01{ M01F01 = Guid.NewGuid(), M01F02 = "New Inspiron 14 plus", M01F03 = "Dell", M01F04 = "13th generation Intel core i7", M01F05 = 32, M01F06 = 47989.98m },
            new COM01{ M01F01 = Guid.NewGuid(), M01F02 = "HP OMEN Gaming Laptop 16-xd0007AX", M01F03 = "HP", M01F04 = "AMD Ryze 7-7840HS", M01F05 = 16, M01F06 = 104600 },
            new COM01{ M01F01 = Guid.NewGuid(), M01F02 = "Alienware x16 Gaming Laptop", M01F03 = "Dell", M01F04 = "13th Gen Intel Core i9-13900HK", M01F05 = 32, M01F06 = 395190.00m },
            new COM01{ M01F01 = Guid.NewGuid(), M01F02 = "HP Victus Gaming Laptop 15-fa0555TX", M01F03 = "HP", M01F04 = "12th Generation Intel Core i5", M01F05 = 16, M01F06 = 73699 },
            new COM01{ M01F01 = Guid.NewGuid(), M01F02 = "XPS 13 Laptop", M01F03 = "Dell", M01F04 = "12th Gen Intel Core i7-1250U", M01F05 = 32, M01F06 = 124490.02m },
        };

        /// <summary>
        /// Validates computer object
        /// </summary>
        /// <param name="objCOM01">Object of class COM01</param>
        /// <returns>True if object is valid, false otherwise</returns>
        public bool Validation(COM01 objCOM01)
        {
            if(objCOM01.M01F02 != null &&  objCOM01.M01F03 != null && objCOM01.M01F04 != null && objCOM01.M01F05>0 && objCOM01.M01F06 > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds computer to the list
        /// </summary>
        /// <param name="objCOM01">Object of class COM01</param>
        /// <returns>Appropriate message</returns>
        public string AddComputer(COM01 objCOM01)
        {
            objCOM01.M01F01 = Guid.NewGuid();

            lstCOM01.Add(objCOM01);

            return "Success";

        }

        /// <summary>
        /// Updates computer into the list
        /// </summary>
        /// <param name="objCOM01">Object of class COM01</param>
        /// <returns>Appropriate message</returns>
        public string? UpdateComputer(COM01 objCOM01)
        {
            var computer = lstCOM01.FirstOrDefault(c=>c.M01F01 == objCOM01.M01F01);
            if(computer != null)
            {
                var index = lstCOM01.FindIndex(c=>c.M01F01==objCOM01.M01F01);
                if(index > -1)
                {
                    lstCOM01[index] = objCOM01;
                    return "Success";
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes computer from list
        /// </summary>
        /// <param name="guid">Guid of computer user wants delete</param>
        /// <returns>Appropriate message</returns>
        public string? DeteletComputer(Guid guid)
        {
            var computer = lstCOM01.FirstOrDefault(c => c.M01F01 == guid);
            if(computer != null)
            {
                lstCOM01.Remove(computer);
                return "Success";
            }
            return null;
        }

    }
}
