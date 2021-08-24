using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using MSSQL_App;

namespace MSSQL_App_New
{
    public class Smoke
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

            var sql1       = "select id from Animals where Age = " + min            + " and Name !=" + "'Prancer'";
            var sql2       = "select id from "                     + GetTableName() + " where id > 1";
            var sql3       = "select id from Animals "             + "where id"     + MinField;
            var sql4 = "select d.Title as DrawbackTitle,"                     +
                       " F.Title as FoodTitle from Drawbacks d "              +
                       "inner join DrawbackFood DF on d.Id = DF.DrawbacksId " +
                       "inner join Food F on DF.FoodsId = F.Id";
            var sql5 = "select d.Title as DrawbackTitle, F.Title as FoodTitle from Drawbacks d " +
                       "inner join DrawbackFood DF on d.Id = DF.DrawbacksId "                    +
                       "inner join Food F on DF.FoodsId = F.Id "                                 +
                       "where d.Id >"                                                            +
                       id                                                                        +
                       " and F.id >="                                                            +
                       id;
            var sql6 = "select id from Animals " +
                       "where id <= "            + id;
            var sql7       = "select t " + "from Animals";
            var sql8 = "select d.Title as DrawbackTitle, F.Title as FoodTitle from Drawbacks d " +
                       "inner join DrawbackFood DF on d.Id = DF.DrawbacksId "                    +
                       "inner join Food F on DF.FoodsId = F.Id "                                 +
                       "where d.Id >"                                                            +
                       InstanceId                                                                +
                       " and "                                                                   +
                       "t = "                                                                    +
                       ConstId;
            var sql9 = "insert into Drawbacks"      +
                        "(Title, Consequence_Name) " +
                        "values ("                   +
                        StaticString                 +
                        ")";
            var sql10 = "insert into Drawbacks"      +
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
            var sql11 = "select id from " + parameterTitle + " where id > 1";
            var sql12 = " INSERT INTO dbo.\"Animals\" " +
                        " (\"Name\", Age "              +
                        ")"                             +
                        " VALUES "                      +
                        $" ('{id.ToString()}', "        +
                        $" (SELECT id FROM Jobs where id = '{id.ToString()}'))";
        }

        private void StringFormat()
        {
            var id           = 1;

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
            var sql8 = string.Format("select Name, Age as \"{0}\" from Animals where Id = {1}", "AllInfo", 1);
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
            var sql3 = "select Name from Animals where" + $" id={GetId()}";
            var sql4 = $@"select J.Title from Jobs J 
inner join JobDrawbacks JD on J.Id = JD.JobId
inner join Drawbacks D on D.Id = JD.DrawbackId
where J.Id = {id} 
and D.Title like {parameterTitle}
order by J.Title";
            var sql5 = $"select * from Clubs " +
                       $"where id = {id}";
            var sql6 = $@"select j.Title from " + $"(select * from Jobs) j";
            var sql7 = $@"
select j.Title from
(
    select id, title from Jobs
    where Title like '{parameterTitle}'
    ) j";
            var sql8 = $"select * from "             +
                       $"(select * from Animals) a " +
                       $"where a.Id={id}";
            var sql9 = $@"insert into Jobs
(Title, Salary) 
values 
       ('{parameterTitle}', {id + 100}),
       ('{ConstString}', 1)";

            var sql10 = $@"insert into Drawbacks
(Title, Consequence_Name) 
select Name, Name 
from Animals
where id = {id}
and Name like '{id}'";
        }

        private void Verbatim(string parameterTitle)
        {
            var id = 1;
            var sql1 = @$"select distinct Age from Animals
where id = {id} or
Name like '{parameterTitle.ToUpper()}' or
LovedById is not null or
Name like '{parameterTitle}'";
            var sql2 = @"
select
""id"" as Id,
""ClubId"" as Club_id
from dbo.Grades
where TheGrade >=" + id;
            var sql3 = $@"select * from Animals where Name like '{nameof(parameterTitle)}'";
            var sql4 = @$"Update dbo.""Drawbacks"" set ""Title"" = '{parameterTitle}' 
where id = {id}";
            const string sql   = @" SELECT distinct name FROM ""Animals"" ";
            var          sql11 = @" SELECT distinct name FROM ""Animals"" ";
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

        private void WithParameters(string stringParameter)
        {
            using var connection = new SqlConnection(Constants.ConnectionString);

            var sql1    = "select Title from Clubs where Id > @SmallId";
            var command = new SqlCommand(sql1, connection);
            command.Parameters.Add(new SqlParameter("@SmallId", 1));

            var sql2 =
                "select a.Name, g.TheGrade from Grades g" +
                " left join Animals a"                    +
                " on g.AnimalId = a.Id"                   +
                " where g.TheGrade > @MinGrade";
            var cmd6 = new SqlCommand(sql2, connection);
            cmd6.Parameters.Add("@MinGrade", SqlDbType.Decimal);
            cmd6.Parameters["@MinGrade"].Value = 4.5;
            
            var sql3 = @$"select * from Animals
            where id = @id and Name like {stringParameter}";
        }

        private void WithDeclaredVariable(string stringParameter)
        {
            var sql1 = @"select * from Animals
declare 
@name varchar(max)
SET @name1 = 'Some%'; 
select * from Animals where Name like @name1";
        }

        private void WithTSqlFunction()
        {
            var sql1 = @"
select * from Animals
DECLARE Animals_Cursor CURSOR FOR  
SELECT Id  
FROM Animals;  
OPEN Animals_Cursor;  
FETCH NEXT FROM Animals_Cursor;  
WHILE @@FETCH_STATUS = 0  
   BEGIN  
      FETCH NEXT FROM Animals_Cursor;  
   END;  
CLOSE Animals_Cursor;  
DEALLOCATE Animals_Cursor;  
GO ";
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

        #endregion
    }

    public static class MyExt
    {
        public static int GetMyId(this string id)
        {
            return Convert.ToInt32(id);
        }
    }
}