using DependencyInjection.Interfaces;
using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    /// <summary>
    /// Implements interface of application's operations
    /// </summary>
    public class OperationsAPL01 : IOperations
    {
        /// <summary>
        /// List of applications
        /// </summary>
        public static List<APL01> lstAPL01 = new List<APL01>();

        /// <summary>
        /// Retrives all applications
        /// </summary>
        /// <returns>List of applications</returns>
        public List<APL01> Select()
        {
            return lstAPL01;
        }

        /// <summary>
        /// Adds application into the list
        /// </summary>
        /// <param name="objAPL01">Object of application to be add</param>
        /// <returns>Appropriate message</returns>
        public string Add(APL01 objAPL01)
        {
            objAPL01.L01F01 = lstAPL01.Count + 1;
            lstAPL01.Add(objAPL01);
            return "Success";
        }

        /// <summary>
        /// Updates application into the list
        /// </summary>
        /// <param name="objAPL01">Object of application to be update</param>
        /// <returns>Appropriate message</returns>
        public string? Update(APL01 objAPL01)
        {
            var applicant = lstAPL01.FirstOrDefault(a => a.L01F01 == objAPL01.L01F01);
            if (applicant == null)
            {
                return null;
            }
            else
            {
                var index = lstAPL01.FindIndex(a=>a.L01F01==objAPL01.L01F01);
                lstAPL01[index] = objAPL01;
                return "Success";
            }
        }

        /// <summary>
        /// Deletes application from list
        /// </summary>
        /// <param name="id">Id of application to be delete</param>
        /// <returns>Appropriate message</returns>
        public string? Delete(int id)
        {
            var applicant = lstAPL01.FirstOrDefault(a => a.L01F01 == id);
            if (applicant == null)
            {
                return null;
            }
            else
            {
                lstAPL01.Remove(applicant);
                return "Success";
            }
        }

    }
}
