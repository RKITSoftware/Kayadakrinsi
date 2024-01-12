﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenAuthorization.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public int NoOfEmployees { get; set; }

        public Company(int id, string name, string city, string type, int noEmp)
        {
            Id = id;
            Name = name;
            City = city;
            Type = type;
            NoOfEmployees = noEmp;
        }

        public static List<Company> GetCompanies()
        {
            var list = new List<Company> {
                new Company(1,"TATA","Jamshedpur","Production",1028000),
                new Company(2,"Microsoft","Washington","IT",221000),
                new Company(3,"Google","California","IT",156000),
                new Company(4,"RKIT","Rajkot","IT",200),
                new Company(5,"Sugar Cosmetics","Mumbai","Production",600)
            };
            return list;
        }
    }
}