using System;
using Microsoft.Data.SqlClient;

namespace MSSQL_App
{
    class Program
    {
        static void Main(string[] args)
        {
            string        connetionString;
            SqlConnection cnn;

            connetionString = "Server=unit-1019\\sqlexpress;Database=BeaversLife;Trusted_Connection=True;" +
                              "MultipleActiveResultSets=True";
            cnn             = new SqlConnection(connetionString);
            cnn.Open();
            
            SqlCommand    command;
            SqlDataReader dataReader;

            string sql;
            string output = "";

            sql        = "select Name from Animals";
            command    = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                output += dataReader.GetValue(0) + "\n";
            }
            
            cnn.Close();
            Console.WriteLine(output);
        }
    }
}