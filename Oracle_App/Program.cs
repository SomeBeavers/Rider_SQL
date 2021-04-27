using System;
using Oracle.ManagedDataAccess.Client;


namespace Oracle_App
{
    class Program
    {
        // C:\app\lilia.shamsutdinova\product\18.0.0
        static void Main(string[] args)
        {
            // TODO: insert password
            OracleConnection con = new OracleConnection("User Id=system;Password=system;Data Source = localhost:1521/XEPDB1;");
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT \'Hello World!\' FROM dual";
 
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Console.WriteLine(reader.GetString(0));
            Console.WriteLine("Hello World!");
        }
    }
}