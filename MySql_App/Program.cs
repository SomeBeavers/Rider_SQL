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

            ExecuteQueries(con);
        }

        private static void ExecuteQueries(MySqlConnection con)
        {
            string    sql3 = "SELECT first_name, gender FROM employees where gender=@gender";
            using var cmd3 = new MySqlCommand(sql3, con);

            cmd3.Parameters.AddWithValue("@gender", 'F');
            cmd3.Prepare();

            using MySqlDataReader rdr = cmd3.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} ", rdr.GetValue(0), rdr.GetValue(1));
            }
        }
        
        private static void AllQueries(MySqlConnection con)
        {
            string sql  = "SELECT * FROM departments where dept_name=\"Sales\"";
            string sql2 = @"SELECT * FROM departments where dept_name=""Sales""";

            string    sql3 = "SELECT first_name, gender FROM employees where gender=@gender";
            using var cmd3  = new MySqlCommand(sql3, con);

            cmd3.Parameters.AddWithValue("@gender", 'F');
            cmd3.Prepare();
        }
    }
}