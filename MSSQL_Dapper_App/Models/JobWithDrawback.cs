using System;
using System.Linq;

namespace MSSQL_Dapper_App.Models
{
    public class JobWithDrawback
    {
        public string JobTitle         { get; set; }
        public string DrawbackTitle    { get; set; }
        public string Consequence_Name { get; set; }
        public int    Salary           { get; set; }
    }
}