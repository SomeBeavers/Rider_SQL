using System;
using CoreLib_Common;

namespace EF_BeaversLife.Queries
{
    // ReSharper disable once InconsistentNaming
    public class UseTVF
    {
        // ReSharper disable once InconsistentNaming
        public void UseTVF1()
        {
            using var context = new AnimalContext();

            var animals = context.Animals;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                var animalLocations = context.GetAnimalLocation(animal.Id);
                foreach (var location in animalLocations)
                {
                    Console.WriteLine(location);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}