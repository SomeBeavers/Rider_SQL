using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FrameworkLib_Common;
using FrameworkLib_Common.Model;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseInclude
    {
        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseDbQueryIncludeWithStringPath()
        {
            using var context  = new AnimalContext();
            var foodList = context.VeganFood
                                  .Include("Drawbacks").Include("Animal.Grades")
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food in foodList)
            {
                Console.WriteLine(food);

                if (food.Drawbacks != null)
                {
                    foreach (var drawback in food.Drawbacks)
                    {
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }

                Console.Write("\t");
                Console.Write("\t");
                Console.WriteLine(food.Animal);

                if (food.Animal?.Grades != null)
                {
                    foreach (var grade in food.Animal.Grades)
                    {
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseExtensionInclude1()
        {
            using var context  = new AnimalContext();
            var foodList = context.VeganFood
                                  .Include(food => food.Drawbacks).Include(food => food.Animal.Grades)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food in foodList)
            {
                Console.WriteLine(food);

                if (food.Drawbacks != null)
                {
                    foreach (var drawback in food.Drawbacks)
                    {
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }

                Console.Write("\t");
                Console.Write("\t");
                Console.WriteLine(food.Animal);

                if (food.Animal?.Grades != null)
                {
                    foreach (var grade in food.Animal.Grades)
                    {
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseInclude1()
        {
            using var context = new AnimalContext();
            var beavers = context.Beavers
                                 .Include(b => b.Clubs)
                                 .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            beavers.ForEach(b =>
            {
                Console.WriteLine(b);
                if (b.Clubs != null)
                {
                    Console.Write("\t");
                    Console.WriteLine(b.Clubs.Count);

                    b.Clubs.ForEach(Console.WriteLine);
                }
            });

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseWhere1()
        {
            using var context = new AnimalContext();
            var beavers = context.Beavers
                                 .Where(b => b.Fluffiness == FluffinessEnum.VeryFluffy)
                                 .Include(b => b.Clubs)
                                 .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            beavers.ForEach(b =>
            {
                Console.WriteLine(b);
                if (b.Clubs != null)
                {
                    Console.Write("\t");
                    Console.WriteLine(b.Clubs.Count);

                    b.Clubs.ForEach(Console.WriteLine);
                }
            });

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is NOT needed as string Include is used.
        /// </summary>
        public async Task UseStringInclude1()
        {
            using var context = new AnimalContext();
            var clubs = context.Clubs
                               .Include("Locations")
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            await clubs.ForEachAsync(c => Console.WriteLine(c.Locations?.Count));

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is NOT needed as string Include is used.
        /// </summary>
        public async Task UseStringIncludeWithWhere1()
        {
            using var context = new AnimalContext();
            var clubs = context.Clubs
                               .Where(c => c.Title.Contains("Corn"))
                               .Include("Locations")
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            await clubs.ForEachAsync(c => Console.WriteLine(c.Locations?.Count));

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}