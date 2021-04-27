using System;
using System.Linq;
using CoreLib_Common;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseRawSql
    {
        public void UseRawSql1()
        {
            using var context = new AnimalContext();

            var foods = context.Food.FromSqlRaw("select * from Food where Title = {0}", "Pizza")
                               .Include(food => food.Animal)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food in foods)
            {
                Console.WriteLine(food);

                Console.Write("\t");
                Console.WriteLine(food.Animal);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseRawSql2(string pizza)
        {
            using var context = new AnimalContext();

            var foods = context.Food.FromSqlInterpolated($"select * from Food where Title = {pizza}")
                    .Include(food => food.Animal)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food in foods)
            {
                Console.WriteLine(food);

                Console.Write("\t");
                Console.WriteLine(food.Animal);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}