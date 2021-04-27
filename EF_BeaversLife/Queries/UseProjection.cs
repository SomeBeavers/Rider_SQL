using System;
using System.Linq;
using CoreLib_Common;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseProjection
    {
        /// <summary>
        /// Include is NOT needed.
        /// </summary>
        public void UseProjection1()
        {
            using var context = new AnimalContext();
            var animals = context.Animals.AsNoTracking()
                                 .OrderByDescending(a => a.Age)
                                 .Select(a => new
                                 {
                                     a.Name,
                                     Clubs = a.Clubs.Select(c => new
                                     {
                                         c.Title,
                                         Grades = c.Grades.Select(g => new
                                         {
                                             g.TheGrade
                                         })
                                     })
                                 });

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.Name);
                foreach (var club in animal.Clubs)
                {
                    Console.Write("\t");
                    Console.WriteLine(club.Title);

                    foreach (var grade in club.Grades)
                    {
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Include is NOT needed.
        /// </summary>
        public void UseProjection2()
        {
            using var context = new AnimalContext();

            var drawbacks = context.Drawbacks.Select(x => new
            {
                Id               = x.Id,
                Consequence_Name = x.Consequence.Name
            }).ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback.Consequence_Name);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}