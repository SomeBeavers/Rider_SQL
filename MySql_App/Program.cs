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

            string    sql = "SELECT * FROM departments where dept_name=\"Sales\"";
            
            string    sql2 = @"SELECT * FROM departments where dept_name=""Sales""";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} ", rdr.GetValue(0), rdr.GetValue(1));
            }
        }
    }
}