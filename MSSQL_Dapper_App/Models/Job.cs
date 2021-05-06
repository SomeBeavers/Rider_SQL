using System;
using System.Linq;

namespace MSSQL_Dapper_App.Models
{
    public class Job
    {
        public int    Id     { get; set; }
        public string Title  { get; set; }
        public int    Salary { get; set; }
    }
}