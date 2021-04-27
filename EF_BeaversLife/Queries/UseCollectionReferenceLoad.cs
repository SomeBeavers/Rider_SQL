using System;
using System.Linq;
using CoreLib_Common;
using CoreLib_Common.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseCollectionReferenceLoad
    {
        /// <summary>
        ///     Include is not needed.
        /// </summary>
        public void UseReferenceLoad()
        {
            using var context = new AnimalContext();
            var       food    = context.Food.Find(1);
            context.Entry(food).Reference(f => f.Animal).Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(food?.Animal);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is not needed.
        /// </summary>
        public void UseStringReferenceLoad()
        {
            using var context = new AnimalContext();
            var       food    = context.Food.Find(1);
            context.Entry(food).Reference("Animal").Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(food?.Animal);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is not needed.
        /// </summary>
        public void UseCollectionLoad()
        {
            using var context = new AnimalContext();
            var       food    = context.Food.Find(1);
            context.Entry(food).Collection(f => f.Drawbacks).Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            food?.Drawbacks?.ToList().ForEach(Console.WriteLine);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is not needed.
        /// </summary>
        public void UseStringCollectionLoad()
        {
            using var context = new AnimalContext();
            var       food    = context.Food.Find(1);
            context.Entry(food).Collection("Drawbacks").Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            food?.Drawbacks?.ToList().ForEach(Console.WriteLine);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     NOTE: Load after .Query() is not working.
        ///     Include is not needed.
        /// </summary>
        public void UseCollectionWithFiltersLoad()
        {
            using var context = new AnimalContext();
            var       food    = context.Food.Find(1);
            context.Entry(food)
                   .Collection(f => f.Drawbacks)
                   .Query()
                   //.Where(d => d.Id == 1)
                   .Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (food is {Drawbacks: { }})
            {
                foreach (var drawback in food.Drawbacks)
                {
                    Console.WriteLine(drawback);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     NOTE: Load after .Query() is not working.
        ///     Include is not needed.
        /// </summary>
        public void UseStringCollectionWithFiltersLoad()
        {
            using var context = new AnimalContext();
            var       food    = context.Food.Find(1);
            context.Entry(food)
                   .Collection<Drawback>("Drawbacks")
                   .Query()
                   .Where(d => d.Id == 1)
                   .Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (food is {Drawbacks: { }})
            {
                foreach (var drawback in food.Drawbacks)
                {
                    Console.WriteLine(drawback);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}