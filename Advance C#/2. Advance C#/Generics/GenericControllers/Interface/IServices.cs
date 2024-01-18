using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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