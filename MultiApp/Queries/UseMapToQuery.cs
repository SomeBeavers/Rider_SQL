using System;
using System.Linq;
using CoreMultiLib;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseMapToQuery
    {
        public void MapToQuery1()
        {
            using var context = new AnimalContext();

            var mapToQueries = context.MapToQuery.Include(x => x.Club);

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var mapToQuery in mapToQueries)
            {
                Console.WriteLine(mapToQuery);
                Console.Write("\t");
                Console.WriteLine(mapToQuery.Club);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}