using System;
using CoreMultiLib;

namespace EF_BeaversLife.Queries
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