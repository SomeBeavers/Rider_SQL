using System;
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
            using (IDbConnection db = new SqlConnection("Server=unit-1019\\sqlexpress;Database=BeaversLife;Trusted_Connection=True;"+
                                                        "MultipleActiveResultSets=True"))
            {
                db.Open();
                var result = db.Query<int>("SELECT COUNT(*) from Animals").Single();
                var result2 = db.Query<int>("select Id from Beavers where Fluffiness = @P1",
                    new { P1 = 1 });
                foreach (var item in result2)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}