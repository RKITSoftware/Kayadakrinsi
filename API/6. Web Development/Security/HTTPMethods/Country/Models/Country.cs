using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Country.Models
{
    public class CountryDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public Double PopulationInM { get; set; }
        public CountryDetails(int id, string name, int code, double populationInM)
        {
            Id = id;
            Name = name;
            Code = code;
            PopulationInM = populationInM;
        }
    }
}