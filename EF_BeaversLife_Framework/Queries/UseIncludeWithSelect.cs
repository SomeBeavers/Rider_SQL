using System;
using System.Data.Entity;
using System.Linq;
using FrameworkLib_Common;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseIncludeWithSelect
    {
        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseIncludeWithSelect1()
        {
            using var context = new AnimalContext();
            var       beavers = context.Beavers.Include(b => b.Clubs.Select(club => club.Locations));

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var beaver in beavers)
            {
                Console.WriteLine(beaver);

                if (beaver.Clubs != null)
                {
                    foreach (var club in beaver.Clubs)
                    {
                        if (club.Locations != null)
                        {
                            foreach (var location in club.Locations)
                            {
                                Console.Write("\t");
                                Console.WriteLine(location);
                            }
                        }
                    }
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
            var animals = context.Animals
                                 .Include(a => a.Food.Drawbacks)
                ;

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