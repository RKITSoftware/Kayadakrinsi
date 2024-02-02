using System.Collections.Generic;

namespace GenericControllers.Interface
{
    /// <summary>
    /// Declare methods for controllers
    /// </summary>
    /// <typeparam name="T">Different class types</typeparam>
    public interface IServices<T> where T : class
    {
        List<T> GetElemets();
        T GetElementById(int id);
        List<T> AddElement(T element);
        List<T> RemoveElement(int id);
        List<T> UpdateElement(T element);
    }
}