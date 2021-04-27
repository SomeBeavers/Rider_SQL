using System;
using System.Linq;
using FrameworkLib_Common;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseNotMapped
    {
        /// <summary>
        ///     Include is not needed.
        /// </summary>
        public void UseNotMapped1()
        {
            using var context = new AnimalContext();
            var       clubs   = context.Clubs;

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var club in clubs)
            {
                Console.WriteLine(club.LocalizedText);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}