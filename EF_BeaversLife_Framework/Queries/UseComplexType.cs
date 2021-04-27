using System;
using System.Linq;
using FrameworkLib_Common;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseComplexType
    {
        /// <summary>
        ///     Include is not needed.
        /// </summary>
        public void UseComplexType1()
        {
            using var context   = new AnimalContext();
            var       drawbacks = context.Drawbacks;

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback);
                Console.Write("\t");
                Console.WriteLine(drawback.DrawbackDetail);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}