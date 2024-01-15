using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryString.Models
{
    public class CompanyV2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public int NoOfEmployees { get; set; }
        public CompanyV2(int id, string name, string country, string type, int noEmp)
        {
            Id = id;
            Name = name;
            Country = country;
            Type = type;
            NoOfEmployees = noEmp;
        }

        public static List<CompanyV2> GetCompanies()
        {
            var list = new List<CompanyV2> {
                new CompanyV2(1,"TATA","India","Production",1028000),
                new CompanyV2(2,"Microsoft","U.S.A.","IT",221000),
                new CompanyV2(3,"Google","U.S.A.","IT",156000),
                new CompanyV2(4,"RKIT","India","IT",200),
                new CompanyV2(5,"Sugar Cosmetics","India","Production",600)
            };
            return list;
        }
    }
}