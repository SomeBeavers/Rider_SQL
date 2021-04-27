using System;
using Microsoft.Data.SqlClient;

namespace MSSQL_App.Queries
{
    public class Select
    {
        public void Select1()
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionString);

            SqlCommand          command;
            string              sql;
            string              output = "";

            sql = "select Name from Animals";

            command = new SqlCommand(sql, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}", reader[0]);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }

            reader.Close();
        }
    }
}