using System;
using System.Linq;
using CoreLib_Common;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseSplitQuery
    {
        /// <summary>
        ///     Split query with Include.
        /// </summary>
        public void UseSplitQuery1()
        {
            using var context = new AnimalContext();

            var animals = context.Animals.AsSplitQuery().Include(f => f.Clubs).ThenInclude(a => a.Grades).Take(1)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                if (animal.Clubs != null)
                    foreach (var club in animal.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
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
        ///     Split query with projection.
        /// </summary>
        public void UseSplitQuery2()
        {
            using var context = new AnimalContext();

            var animals = context.Animals.AsSplitQuery().Select(a => new
                {
                    a.Name, a.Clubs
                }).Take(1)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                if (animal.Clubs != null)
                    foreach (var club in animal.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}