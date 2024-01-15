using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryString.Models
{
    public class CompanyV1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public int NoOfEmployees { get; set; }
        public CompanyV1(int id, string name, string city, string type, int noEmp)
        {
            Id = id;
            Name = name;
            City = city;
            Type = type;
            NoOfEmployees = noEmp;
        }

        public static List<CompanyV1> GetCompanies()
        {
            var list = new List<CompanyV1> {
                new CompanyV1(1,"TATA","Jamshedpur","Production",1028000),
                new CompanyV1(2,"Microsoft","Washington","IT",221000),
                new CompanyV1(3,"Google","California","IT",156000),
                new CompanyV1(4,"RKIT","Rajkot","IT",200),
                new CompanyV1(5,"Sugar Cosmetics","Mumbai","Production",600)
            };
            return list;
        }
    }
}