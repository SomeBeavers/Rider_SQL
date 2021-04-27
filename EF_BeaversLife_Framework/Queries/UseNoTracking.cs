using System;
using System.Data.Entity;
using System.Linq;
using FrameworkLib_Common;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseNoTracking
    {
        /// <summary>
        ///     Include is needed cause entities are not tracked in context.
        /// </summary>
        public void UseNoTracking1()
        {
            using var context = new AnimalContext();
            var       crows   = context.Crows.Include(crow => crow.Grades).AsNoTracking().ToList();

            crows = context.Crows.ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var crow in crows)
            {
                Console.WriteLine(crow);

                if (crow.Grades != null)
                {
                    foreach (var grade in crow.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}