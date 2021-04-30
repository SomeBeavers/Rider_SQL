using System;
using Microsoft.Data.SqlClient;

namespace MSSQL_App.Queries
{
    public class Select
    {
        /// <summary>
        /// Use simple one column string select.
        /// </summary>
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

        /// <summary>
        /// Use command.Parameters.
        /// </summary>
        public void SelectWithParameters()
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionString);

            SqlCommand command;
            string     sql;
            string     output = "";

            sql = "select Title from Clubs where Id > @SmallId";

            command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@SmallId", 1));

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