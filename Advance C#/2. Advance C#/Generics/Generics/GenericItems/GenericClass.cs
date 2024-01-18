using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Generics.Models;

namespace Generics.GenericItems
{
    public class GenericClass<T>
    {
        public List<T> lstUsers;

        public  GenericClass()
        {
            lstUsers = new List<T>();
        }
        public void AddItem(T item)
        {
            lstUsers.Add(item);
        }
        public void AddItems(GenericClass<T> items) 
        { 
            lstUsers.AddRange((IEnumerable<T>)items);
        }
    }
}