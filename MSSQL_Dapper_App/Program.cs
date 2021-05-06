using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MSSQL_Dapper_App.Models;

namespace MSSQL_Dapper_App
{
    class Program
    {
        static void Main(string[] args)
        {
            using IDbConnection db = new SqlConnection("Server=unit-1019\\sqlexpress;Database=BeaversLife;Trusted_Connection=True;"+
                                                       "MultipleActiveResultSets=True");
            db.Open();
            TestQuery(db);
            db.Close();
        }

        private static void TestQuery(IDbConnection db)
        {
            string sql1 = @"select * from Food f inner  join Animals a on f.AnimalId = a.Id";
            var    data = db.Query<Food, Animal, Food>(sql1, (food, animal) => { food.Animal = animal; return food;});
            foreach (var i in data)
            {
                Console.WriteLine(i.Animal.Age);
            }
        }

        private static void AllQueries(IDbConnection db)
        {
            var result = db.Query<int>("SELECT COUNT(*) from Animals").Single();
            
            var result2 = db.Query<int>("select Id from Beavers where Fluffiness = @P1",
                new { P1 = 1 });

            var query1            = "select * from Jobs where Salary >= @Salary";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Salary", 10);
            IEnumerable<dynamic> jobs = db.Query(query1, dynamicParameters);
            
            var              parameter2 = new { title = "Messenger", salary = 1 };
            IEnumerable<Job> jobs2      = db.Query<Job>("select * from Jobs where Title = @title and Salary >= @salary", parameter2);
            string           s1         = "select * from Jobs where \"Title\" = 'Messenger' and Salary >=10";

            var jobWithDrawbacks = db.Query<JobWithDrawback>("select j.Salary, j.Title as JobTitle, dr.Title as DrawbackTitle, dr.Consequence_Name from Jobs j\ninner join JobDrawbacks jd\non j.Id = jd.JobId\ninner join Drawbacks dr\non jd.DrawbackId = dr.Id\norder by j.Salary");
            
            // BUG: " are not inserted when Enter in long string
            var jobWithDrawbacksSplitted = db.Query<JobWithDrawback>("select j.Salary, j.Title as JobTitle, dr.Title as DrawbackTitle, dr.Consequence_Name from Jobs j " 
                                                                     + "inner join JobDrawbacks jd" +
                                                                     "on j.Id = jd.JobId"           +
                                                                     "inner join Drawbacks dr"      +
                                                                     "on jd.DrawbackId = dr.Id"     +
                                                                     "order by j.Salary");
            
            // List Support
            db.Query<int>(
                "select * from (select 1 as Id union all select 2 union all select 3) as X where Id in @Ids"
                , new { Ids = new int[] { 1, 2, 3 } });
            
            // Literal replacements
            db.Query("select * from Jobs where Salary = {=salary}", new { salary=10 });
            
            
            // Execute command multiple times
            int inserted1 = db.Execute(@"insert into Jobs (Title, Salary) Values (@Title, @Salary)",
                new []
                {
                    new
                    {
                        Title = "title1",
                        Salary = 100
                    },
                    new
                    {
                        Title = "title2",
                        Salary = 200
                    },
                    new
                    {
                        Title = "title3",
                        Salary = 300
                    },
                });

            string sql1 = @"select * from Food f inner  join Animals a on f.AnimalId = a.Id";
            var    data = db.Query<Food, Animal, Food>(sql1, (food, animal) => { food.Animal = animal; return food;});
            
            var sql2 =
                @"
select * from Food where AnimalId = @id
select * from Clubs where Id = @id
select * from Jobs where Id = @id";
            db.QueryMultiple(sql2, new { id = 1 });
        }
    }
}