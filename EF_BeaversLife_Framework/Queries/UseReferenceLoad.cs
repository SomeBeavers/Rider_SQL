using System;
using System.Linq;
using FrameworkLib_Common;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseReferenceLoad
    {
        public void UseReferenceLoad1()
        {
            using var context = new AnimalContext();
            var       animal  = context.Animals.First(a => a.Name == "SomeBeavers1");

            //  Include is not needed due to Load()
            context.Entry(animal).Collection(a => a.Grades).Load();

            Console.ForegroundColor = ConsoleColor.Magenta;

            if (animal.Grades != null)
            {
                foreach (var animalGrade in animal.Grades)
                {
                    Console.WriteLine(animalGrade);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}