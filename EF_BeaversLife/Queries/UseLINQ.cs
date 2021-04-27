using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLib_Common;
using CoreLib_Common.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseLinq
    {
        /// <summary>
        ///     Async method with await foreach and LINQ.
        /// </summary>
        public async Task UseLinq1()
        {
            await using var context = new AnimalContext();

            var beavers =
                    //(
                    from b in context.Beavers
                    where b.Fluffiness == FluffinessEnum.VeryFluffy
                    select b
                //)
                //.Include("Clubs")
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            await foreach (var beaver in beavers.AsAsyncEnumerable())
            {
                Console.WriteLine(beaver);

                if (beaver.Clubs != null)
                    foreach (var club in beaver.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}