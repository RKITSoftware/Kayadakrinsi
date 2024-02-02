using System.Collections;
using System.Collections.Generic;

namespace Generics.GenericItems
{
    /// <summary>
    /// Declares generic class
    /// </summary>
    /// <typeparam name="T">type of class</typeparam>
    public class GenericClass<T> : IEnumerable<T>
    {
        /// <summary>
        /// Declares generic list
        /// </summary>
        public List<T> lstUsers;

        /// <summary>
        /// Defines generic list
        /// </summary>
        public  GenericClass()
        {
            lstUsers = new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return lstUsers.GetEnumerator();
        }

        /// <summary>
        /// Implement non-generic GetEnumerator method required by IEnumerable
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds one item to list
        /// </summary>
        /// <param name="item">object which will be added to the list</param>
        public void AddItem(T item)
        {
            lstUsers.Add(item);
        }

        /// <summary>
        /// Adds multiple items to list
        /// </summary>
        /// <param name="items">List of items which will be added to the list</param>
        public void AddItems(GenericClass<T> items)
        {
            foreach (T item in items)
            {
                lstUsers.Add(item);
            }
        }
    }
}