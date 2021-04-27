using System;
using System.Linq;
using CoreMultiLib;
using CoreMultiLib.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseAsEnumerable
    {
        /// <summary>
        ///     AsEnumerable for client evaluation.
        /// </summary>
        public void UseAsEnumerable1()
        {
            using var context = new AnimalContext();

            var drawbacks = context.Drawbacks.Include(d => d.Clubs).Where(d => d.Clubs.Count > 2)
                .AsEnumerable()
                .Where(drawback => IsLongTitle(drawback));

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback);
                if (drawback.Clubs != null)
                    foreach (var club in drawback.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include when nav property is used in client evaluated Where.
        /// </summary>
        public void UseAsEnumerable2()
        {
            using var context = new AnimalContext();

            var grades = context.Grades
                .Include(grade => grade.Animal)
                .AsEnumerable()
                // use IsBeaver method
                .Where(grade => IsAcceptable(grade)).Where(grade => grade.Animal is Beaver);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var grade in grades)
            {
                Console.WriteLine(grade);

                Console.Write("\t");
                Console.WriteLine(grade.Animal);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // ReSharper disable once UnusedMember.Local
        private bool IsBeaver(Grade grade)
        {
            return grade.Animal is Beaver;
        }

        private bool IsAcceptable(Grade grade)
        {
            return grade.TheGrade >= 4.5;
        }

        private bool IsLongTitle(Drawback drawback)
        {
            return drawback.Title.Length > 5;
        }
    }
}