using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FrameworkLib_Common;
using FrameworkLib_Common.Model;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseAsync
    {
        public async Task UseAsync1()
        {
            using var context = new AnimalContext();

            var foodList = await (from food in context.Food
                                  orderby food.Title
                                  select food)
                // Include is needed here
                //.Include(food => food.Drawbacks)
                .ToListAsync();

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
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public async Task UseAsync2()
        {
            using var context = new AnimalContext();

            var drawbacks = await (from drawback in context.Drawbacks
                                   orderby drawback.Title
                                   select drawback)
                .ToListAsync();

            var foodList = await (from food in context.Food
                                  orderby food.Title
                                  select food)
                // Include is needed here
                //.Include(food => food.Drawbacks)
                .ToListAsync();

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
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public async Task UseAsync3()
        {
            using var context = new AnimalContext();

            var animals = await (from animal in context.Animals
                                   orderby animal.Name
                                   join job in context.Jobs on animal.JobId equals job.Id
                                   select new {animal.Name, job.Title}
                                   )
                .ToListAsync();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}