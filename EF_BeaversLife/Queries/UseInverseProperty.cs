using System;
using System.Linq;
using CoreLib_Common;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseInverseProperty
    {
        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseInverseProperty1()
        {
            using var context = new AnimalContext();
            var       persons = context.Persons.Include(p => p.AnimalsLoved).Include(p => p.AnimalsHated);

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var person in persons)
            {
                Console.WriteLine(person);
                foreach (var animalLoved in person.AnimalsLoved)
                {
                    Console.Write("\t");
                    Console.WriteLine(animalLoved);
                }

                foreach (var animalHated in person.AnimalsHated)
                {
                    Console.Write("\t");
                    Console.Write("\t");
                    Console.WriteLine(animalHated);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}