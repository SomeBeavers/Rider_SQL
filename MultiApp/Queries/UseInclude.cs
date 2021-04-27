using System;
using System.Collections.Generic;
using System.Linq;
using CoreMultiLib;
using CoreMultiLib.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseInclude
    {
        public void UseFilteredInclude1()
        {
            using var context = new AnimalContext();

            var clubs = context.Clubs
                               .Include(club => club.Animals.Where(animal => animal.Name.Contains("Beaver")))
                               .ThenInclude(animal =>
                                   animal.Grades.Where(grade => grade.TheGrade > 4))
                               .Include(club => club.Animals.Where(animal => animal.Name.Contains("Beaver")))
                               .ThenInclude(a => a.Food).ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var club in clubs)
            {
                Console.WriteLine(club);
                if (club.Animals != null)
                    foreach (var animal in club.Animals)
                    {
                        Console.Write("\t");
                        Console.WriteLine(animal);


                        Console.Write("\t");
                        Console.Write("\t");
                        Console.WriteLine(animal.Food);

                        if (animal.Grades != null)
                            foreach (var grade in animal.Grades)
                            {
                                Console.Write("\t");
                                Console.Write("\t");
                                Console.WriteLine(grade);
                            }
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseFilteredInclude2()
        {
            using var context = new AnimalContext();

            var animals =
                    context.Animals.Include(animal => animal.Grades.OrderByDescending(grade => grade.TheGrade).Take(2))
                //.ThenInclude(grade => grade.Club)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                if (animal.Grades != null)
                    foreach (var grade in animal.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);

                        Console.Write("\t");
                        Console.Write("\t");
                        // TODO: Include me
                        Console.WriteLine(grade.Club);
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseStringInInclude1()
        {
            using var context = new AnimalContext();

            var animals = context.Animals.Include("Clubs").Include("Grades");

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
                if (animal.Grades != null)
                    foreach (var grade in animal.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseStringInInclude2()
        {
            using var context = new AnimalContext();

            var animals = context.Animals.AsQueryable();

            string includeProperties = "Clubs,Grades";
            foreach (var includePro in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                animals = animals.Include(includePro);
            }

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
                if (animal.Grades != null)
                    foreach (var grade in animal.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is not needed as string Include is used.
        /// </summary>
        public void UseStringIncludeMultilevel()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals.Include("Food.Drawbacks");

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                Console.Write("\t");
                Console.WriteLine(animal.Food);

                if (animal.Food is {Drawbacks: { }})
                {
                    foreach (var drawback in animal.Food.Drawbacks)
                    {
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseIncludeMultilevel()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals.Include(a => a.Food.Drawbacks);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                Console.Write("\t");
                Console.WriteLine(animal.Food);

                if (animal.Food is {Drawbacks: { }})
                {
                    foreach (var drawback in animal.Food.Drawbacks)
                    {
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}