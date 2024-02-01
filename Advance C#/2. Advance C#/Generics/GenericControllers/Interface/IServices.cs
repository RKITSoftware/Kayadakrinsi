using System.Collections.Generic;

namespace GenericControllers.Interface
{
    public interface IServices<T> where T : class
    {
        List<T> GetElemets();
        T GetElementById(int id);
        List<T> AddElement(T element);
        List<T> RemoveElement(int id);
        List<T> UpdateElement(T element);
    }
}