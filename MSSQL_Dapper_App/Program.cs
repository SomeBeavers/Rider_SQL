using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

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
            var              parameter2 = new { title = "Messenger", salary = 1 };
            IEnumerable<Job> jobs2      = db.Query<Job>("select * from Jobs where Title = @title and Salary >= @salary", parameter2);

            foreach (var job in jobs2)
            {
                Console.WriteLine(job.Title);
            }
        }

        private static void Queries(IDbConnection db)
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
        }
    }

    internal class JobWithDrawback
    {
        public string JobTitle         { get; set; }
        public string DrawbackTitle    { get; set; }
        public string Consequence_Name { get; set; }
        public int    Salary           { get; set; }
    }

    class Job
    {
        public int    Id     { get; set; }
        public string Title  { get; set; }
        public int    Salary { get; set; }
    }
}