using DependencyInjection.Models;

namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Interface for performing operations on applications
    /// </summary>
    public interface IOperations
    {
        /// <summary>
        /// Retrives all applications
        /// </summary>
        /// <returns>List of applications</returns>
        List<APL01> Select();

        /// <summary>
        /// Adds application into the list
        /// </summary>
        /// <param name="objAPL01">Object of application to be add</param>
        /// <returns>Appropriate message</returns>
        string Add(APL01 objAPL01);

        /// <summary>
        /// Updates application into the list
        /// </summary>
        /// <param name="objAPL01">Object of application to be update</param>
        /// <returns>Appropriate message</returns>
        string? Update(APL01 objAPL01);

        /// <summary>
        /// Deletes application from list
        /// </summary>
        /// <param name="id">Id of application to be delete</param>
        /// <returns>Appropriate message</returns>
        string? Delete(int id);
    }
}
