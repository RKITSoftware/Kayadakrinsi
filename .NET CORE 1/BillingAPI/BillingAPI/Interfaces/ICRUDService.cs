using BillingAPI.Enums;
using BillingAPI.Models;

namespace BillingAPI.Interfaces
{
    /// <summary>
    /// Defines Interface for CRUD operations
    /// </summary>
    /// <typeparam name="T">Type of object on which CRUD will be performed</typeparam>
    public interface ICRUDService<T> where T : class
    {
        /// <summary>
        /// Instance of type T
        /// </summary>
        public T obj { get; set; }

        /// <summary>
        /// Defines operation type to be perform
        /// </summary>
        public enmOperations Operations { get; set; }

        /// <summary>
        /// Checks weather object of type T is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExists(int id);

        /// <summary>
        /// Saves the object of type T
        /// </summary>
        /// <returns>Response indicating the result of the save operation</returns>        
        public Response Save();

        /// <summary>
        /// Deletes object of type T
        /// </summary>
        /// <param name="id">Id of object to be delete</param>
        /// <returns>Response indicating the result of the delete operation</returns>
        public Response Delete(int id);

        /// <summary>
        /// Retrieves all objects of type T
        /// </summary>
        /// <returns>Response containing all objects of type T</returns>
        public Response Select();

        /// <summary>
        ///  Retrieves all objects of type T
        /// </summary>
        /// <returns>List containing all objects of type T</returns>
        public List<T> SelectList();
    }
}
