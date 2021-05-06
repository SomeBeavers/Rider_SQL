using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;


namespace Oracle_App
{
    class Program
    {
        // C:\app\lilia.shamsutdinova\product\18.0.0
        static void Main(string[] args)
        {
            // TODO: insert password BeaversLife1 BeaversLifeDB
            // mysql root root
            OracleConnection con = new OracleConnection("User Id=system;Password=system;Data Source = localhost:1521/XEPDB1;");
            con.Open();
            var cmd = ExecuteQueries(con);

            OracleDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} ", rdr.GetValue(0), rdr.GetValue(1));
            }
        }

        private static OracleCommand ExecuteQueries(OracleConnection con)
        {
            OracleCommand cmd2 = con.CreateCommand();
            string               sql2 = "select p.product_name, p.list_price from products p where p.product_name LIKE :name";
            OracleParameter      param2 = new OracleParameter("name", OracleDbType.Varchar2);
            param2.Value     = "AMD%";
            
            cmd2.CommandText = sql2;
            cmd2.Parameters.Add(param2);
            
            return cmd2;
        }
        
        private static void AllQueries(OracleConnection con)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT 'Hello World!' FROM dual";
            cmd.CommandText = "SELECT COUNT(*) FROM ORDER_ITEMS";

            // TODO: press Enter to split string
            string          sql1       = "select count(*) from products p group by p.product_name";

            OracleCommand cmd2 = con.CreateCommand();
            string               sql2 = "select p.product_name, p.list_price from products p where p.product_name LIKE :name";
            OracleParameter      param2 = new OracleParameter("name", OracleDbType.Varchar2);
            param2.Value = "AMD%";
            
            cmd2.CommandText = sql2;
            cmd2.Parameters.Add(param2);
            
            
        }
    }
}