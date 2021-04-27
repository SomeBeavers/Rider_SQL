using System;
using System.Linq;
using CoreLib_Common;
using EF_BeaversLife.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseExtension
    {
        public void UseExtension1()
        {
            using var context = new AnimalContext();

            var clubs = context.Clubs.Include(c => c.Drawbacks).IncludeGradesAndAnimal();
            
            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var club in clubs)
            {
                // Include is not needed.
                Console.WriteLine(club.Animals);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}