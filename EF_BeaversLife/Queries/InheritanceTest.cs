using System;
using CoreLib_Common;
using CoreLib_Common.Model;

namespace EF_BeaversLife.Queries
{
    public class InheritanceTest
    {
        /// <summary>
        /// Cast to derived type.
        /// Include is needed.
        /// </summary>
        public void RSRP_481554_1()
        {
            using var context     = new AnimalContext();
            using var transaction = context.Database.BeginTransaction();

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var food in context.Food)
            {
                var normalFood = (NormalFood) food;
                Console.WriteLine(normalFood);
                if (normalFood.Drawbacks != null)
                {
                    foreach (var drawback in normalFood.Drawbacks)
                    {
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            transaction.Commit();
        }
    }
}