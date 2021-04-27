using System;
using Microsoft.Data.SqlClient;
using MSSQL_App.Queries;

namespace MSSQL_App
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteQueries();
        }

        private static void ExecuteQueries()
        {
            new Select().Select1();
        }
    }
}