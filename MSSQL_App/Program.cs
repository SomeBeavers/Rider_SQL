using System;
using System.Data;
using Microsoft.Data.SqlClient;

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
            //new Select().Select1();
            using SqlConnection connection = new SqlConnection(Constants.ConnectionString);

            var command5 = new SqlCommand();
            command5.Connection  = connection;
            command5.CommandText = "select @AllSalary = SUM(Salary) from Jobs where Salary >@MinSalary";
            command5.CommandType = CommandType.Text;

            var parameter5_in = new SqlParameter();
            parameter5_in.ParameterName = "@MinSalary";
            parameter5_in.Value         = 1;
            command5.Parameters.Add(parameter5_in);
            command5.Parameters.Add("@AllSalary", SqlDbType.Int).Direction = ParameterDirection.Output;
            

            connection.Open();
            command5.ExecuteNonQuery();
            Console.WriteLine(Convert.ToInt32(command5.Parameters["@AllSalary"].Value));

            // using SqlDataReader reader = command4.ExecuteReader();
            //
            // if (reader.HasRows)
            // {
            //     while (reader.Read())
            //     {
            //         Console.WriteLine("{0} {1} {2}", reader[0], reader[1], reader[2]);
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("No rows found.");
            // }
            //
            // reader.Close();
        }

        private static void AllQueries()
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionString);

            var        sql     = "select Title from Clubs where Id > @SmallId";
            var command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@SmallId", 1));
            
            var          sql2      = "select Title from Clubs where Id > @SmallId";
            var          command2  = new SqlCommand(sql, connection);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@WrongName"; // change to @SmallId
            parameter2.Value         = 1;
            command2.Parameters.Add(parameter2);
            
            SqlCommand command3 = new SqlCommand();
            command3.Connection  = connection;
            command3.CommandText = "sp_executesql";
            command3.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@sql";
            parameter3.SqlDbType     = SqlDbType.NVarChar;
            parameter3.Value         = "select * from Jobs where Salary > @salary";            
            SqlParameter parameter3_1 = new SqlParameter();
            parameter3_1.ParameterName = "@salary"; 
            parameter3_1.Value         = 1;       
            SqlParameter parameter3_2 = new SqlParameter();
            parameter3_2.ParameterName = "@parameters"; 
            parameter3_2.SqlDbType     = SqlDbType.NVarChar;
            parameter3_2.Value         = "@salary int";
            command3.Parameters.Add(parameter3);
            command3.Parameters.Add(parameter3_2);
            command3.Parameters.Add(parameter3_1);
            
            var pizza4      = new SqlParameter("parameter", "Pizza");
            var queryString = "select * from Food where Title = @parameter";
            // var foods4 = context.Food.FromSqlRaw(queryString, pizza4)
            //                     .Include(food => food.Animal);
            
            string longQuery1 = "select j.Salary, j.Title as JobTitle, dr.Title as DrawbackTitle, dr.Consequence_Name from Jobs j"+
                                "inner join JobDrawbacks jd"+
                                "on j.Id = jd.JobId"+
                                "inner join Drawbacks dr"+
                                "on jd.DrawbackId = dr.Id"+
                                "order by j.Salary";
            string concatenation    = "select Id, Title from Drawbacks where Title = " + "Test";
            string test             = "Test";
            string interpolaction   = $"select Id, Title from Drawbacks where Title = {test}";
            string verbatimString   = @"select Id, Title from Drawbacks where Title = " + "Test";
            string withPlaceholder  = @"select Id, Title from Drawbacks where Title = {0}";
            string withPlaceholder2 = string.Format("select Id, Title from Drawbacks where Title = '{0}'", 1);
            string withPlaceholder3 = string.Format("select Id, Title from Drawbacks where Title = {0}",   1);

            var command4 = new SqlCommand();
            command4.Connection  = connection;
            command4.CommandText = "GetSalarySum";
            command4.CommandType = CommandType.StoredProcedure;

            var parameter4_in = new SqlParameter();
            parameter4_in.ParameterName = "@MinSalary";
            parameter4_in.Value         = 1;
            command4.Parameters.Add(parameter4_in);
            command4.Parameters.Add("@AllSalary", SqlDbType.Int).Direction = ParameterDirection.Output;
            

            connection.Open();
            command4.ExecuteNonQuery();
            Console.WriteLine(Convert.ToInt32(command4.Parameters["@AllSalary"].Value));
            
            var command5 = new SqlCommand();
            command5.Connection  = connection;
            command5.CommandText = "select @AllSalary = SUM(Salary) from Jobs where Salary >@MinSalary";
            command5.CommandType = CommandType.Text;

            var parameter5_in = new SqlParameter();
            parameter5_in.ParameterName = "@MinSalary";
            parameter5_in.Value         = 1;
            command5.Parameters.Add(parameter5_in);
            command5.Parameters.Add("@AllSalary", SqlDbType.Int).Direction = ParameterDirection.Output;
            

            connection.Open();
            command5.ExecuteNonQuery();
            Console.WriteLine(Convert.ToInt32(command5.Parameters["@AllSalary"].Value));
        }
    }
}