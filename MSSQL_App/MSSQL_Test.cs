using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace MSSQL_App
{
    public class MSSQL_Test
    {
        public const int ConstId = 1;

        public const            string ConstString  = "'a'";
        private static readonly string MinField     = "=1";
        public static           int    StaticId     = 1;
        public static           string StaticString = "'test','test'";
        public                  int    InstanceId   = 1;


        private void Concatenation(string parameterTitle)
        {
            var min         = 1;
            var id          = 1;
            var localString = "'b'";

            var sql1       = "select id from Animals where Age = " + 1;
            var sql1_const = "select id from Animals where Age = 1";

            var sql2       = "select id from Animals where Age = " + min + " and Name !=" + "'Prancer'";
            var sql2_const = "select id from Animals where Age = 1 and Name !='Prancer'";

            var sql3       = "select id from " + GetTableName() + " where id > 1";
            var sql3_const = "select id from Animals where id > 1";

            var sql4       = "select id from Animals " + "where id" + MinField;
            var sql4_const = "select id from Animals where id=1";

            var sql5 = "select d.Title as DrawbackTitle,"                     +
                       " F.Title as FoodTitle from Drawbacks d "              +
                       "inner join DrawbackFood DF on d.Id = DF.DrawbacksId " +
                       "inner join Food F on DF.FoodsId = F.Id";
            var sql5_const =
                "select d.Title as DrawbackTitle, F.Title as FoodTitle from Drawbacks d inner join DrawbackFood DF on d.Id = DF.DrawbacksId inner join Food F on DF.FoodsId = F.Id";

            var sql6 = "select d.Title as DrawbackTitle, F.Title as FoodTitle from Drawbacks d " +
                       "inner join DrawbackFood DF on d.Id = DF.DrawbacksId "                    +
                       "inner join Food F on DF.FoodsId = F.Id "                                 +
                       "where d.Id >"                                                            +
                       id                                                                        +
                       " and F.id >="                                                            +
                       id;
            var sql6_const = @"
                        select d.Title as DrawbackTitle, F.Title as FoodTitle from Drawbacks d 
                        inner join DrawbackFood DF on d.Id = DF.DrawbacksId 
                        inner join Food F on DF.FoodsId = F.Id
                        where d.Id >1 and F.id >=1";

            var sql7 = "select id from Animals " +
                       "where id <= "            + id;
            var sql7_const = "select id from Animals where id <= 1";

            var sql8       = "select t " + "from Animals";
            var sql8_const = "select t from Animals";

            var sql9 = "select d.Title as DrawbackTitle, F.Title as FoodTitle from Drawbacks d " +
                       "inner join DrawbackFood DF on d.Id = DF.DrawbacksId "                    +
                       "inner join Food F on DF.FoodsId = F.Id "                                 +
                       "where d.Id >"                                                            +
                       InstanceId                                                                +
                       " and "                                                                   +
                       "t = "                                                                    +
                       ConstId;

            var sql10 = "insert into Drawbacks"      +
                        "(Title, Consequence_Name) " +
                        "values ("                   +
                        parameterTitle               +
                        ","                          +
                        ConstString                  +
                        ")";

            var sql11 = "insert into Drawbacks"      +
                        "(Title, Consequence_Name) " +
                        "values ("                   +
                        StaticString                 +
                        ")";

            var sql12 = "insert into Drawbacks"      +
                        "(Title, Consequence_Name) " +
                        "values ("                   +
                        parameterTitle               +
                        ","                          +
                        ConstString                  +
                        "),("                        +
                        StaticString                 +
                        ","                          +
                        "'test'"                     +
                        ")";
            var sql13 = "update Animals set " +
                        "Name ="              +
                        localString           +
                        " where Id="          +
                        id;
            var sql14 = "select top(5) Name from Animals " +
                        "where IpAddress like "            +
                        localString                        +
                        " escape "                         +
                        localString;
            var sql15 = "select * from Jobs " +
                        "where Title in ("    +
                        localString           +
                        ","                   +
                        parameterTitle        +
                        ")";
            var sql16 = "Delete from AnimalClub " +
                        "where ClubId="           +
                        id;

            var sql17 = "select id from " + parameterTitle + " where id > 1";
        }

        private void StringFormat()
        {
            var id           = 1;
            var animalsTable = "Animals";

            var sql1 = string.Format("select {0} from Animals where {0} = {1}",   ConstString, id);
            var sql2 = string.Format("select id from Jobs where Salary = {0}{1}", 1,           2);
            var sql3 = string.Format("select * from Clubs where id {0}",          "= 1");
            var sql4 = string.Format("select * from Clubs {0}",                   "where id = 1");
            var sql5 = string.Format("select Age from Animals where Id={0} ",     id);

            var sql6 = string.Format("select j.Title from Jobs j "            +
                                     "inner join Animals A on j.Id = A.JobId" +
                                     " where j.Id>{0} or j.Id = {1}",
                id, id);
            var sql7 = string.Format(@"select * from Drawbacks where 
            Title in ({0}, {1})",
                StaticString, "name");
            var sql8 = string.Format("select Name + Age as \"{0}\" from Animals where Id = {1}", "AllInfo", 1);

            var stringUsedInFormat = "select * from Animals where Age between {0} and {1}";
            var sql9               = string.Format(stringUsedInFormat, 1, 100);
            var sql10 = string.Format("select * from Food f "                      +
                                      "inner join Animals A on f.AnimalId = A.Id " +
                                      "where f.Id<>{0} "                           +
                                      "and f.Title Like {1}", id, "'B%'");
        }

        private void StringInterpolation(string parameterTitle)
        {
            var id   = 1;
            var sql1 = $"select Age from Animals where id > {id} and Id < {id + 10}";
            var sql2 = @$"select sum(Salary) as {parameterTitle} from Jobs ""renamed"" 
inner join Animals A on renamed.Id = A.JobId";
            var sql3 = @$"Delete from Jobs 
where Id=
      {id}
      and Title like {"'%a'"}";
            var sql4 = "select Name from Animals where" + $" id={GetId()}";
        }

        private void Verbatim(string parameterTitle)
        {
        }

        private void Escape()
        {
            var sql1       = "select \"id\" from Animals";
            var sql2       = @"select ""id"" as Id from Animals";
            var sql3       = @"select * from dbo.""Animals""";
            var columnName = "id";
            var sql4       = $"select \"{columnName}\" from Animals";
            var sql5       = @$"select ""{columnName}"" from Animals";
        }

        private void WithParameters()
        {
            using var connection = new SqlConnection(Constants.ConnectionString);

            var sql1    = "select Title from Clubs where Id > @SmallId";
            var command = new SqlCommand(sql1, connection);
            command.Parameters.Add(new SqlParameter("@SmallId", 1));

            var sql2       = "select Title from Clubs where Id > @SmallId";
            var command2   = new SqlCommand(sql2, connection);
            var parameter2 = new SqlParameter();
            parameter2.ParameterName = "@WrongName"; // change to @SmallId
            parameter2.Value         = 1;
            command2.Parameters.Add(parameter2);

            var sql3 =
                "select a.Name, g.TheGrade from Grades g" +
                " left join Animals a"                    +
                " on g.AnimalId = a.Id"                   +
                " where g.TheGrade > @MinGrade";
            var cmd6 = new SqlCommand(sql3, connection);
            cmd6.Parameters.Add("@MinGrade", SqlDbType.Decimal);
            cmd6.Parameters["@MinGrade"].Value = 4.5;
        }

        #region Helpers

        private static string GetTableName()
        {
            return "Animals";
        }

        public static int GetId()
        {
            return 1;
        }

        public static void Smoke_ExecuteQueries()
        {
            // replace SQl
            var columnName = "id";
            var sql        = $"select \"{columnName}\" from Animals";

            using var connection = new SqlConnection(Constants.ConnectionString);

            var command5 = new SqlCommand();
            command5.Connection = connection;

            command5.CommandText = sql;
            command5.CommandType = CommandType.Text;
            //
            // var parameter5_in = new SqlParameter();
            // parameter5_in.ParameterName = "@MinSalary";
            // parameter5_in.Value         = 1;
            // command5.Parameters.Add(parameter5_in);
            // command5.Parameters.Add("@AllSalary", SqlDbType.Int).Direction = ParameterDirection.Output;
            //

            connection.Open();
            // command5.ExecuteNonQuery();
            // Console.WriteLine(Convert.ToInt32(command5.Parameters["@AllSalary"].Value));

            using var reader = command5.ExecuteReader();

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

        #endregion
    }
}