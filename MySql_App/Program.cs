using System;
using MySql.Data.MySqlClient;

namespace MySql_App
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = @"server=localhost;userid=root;password=root;database=employees";

            using var con = new MySqlConnection(cs);
            con.Open();

            Console.WriteLine($"MySQL version : {con.ServerVersion}");
        }
    }
}