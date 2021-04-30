using System;
using System.Data.SqlClient;
using System.Linq;
using FrameworkLib_Common;
using FrameworkLib_Common.Model;

namespace EF_BeaversLife_Framework.Queries
{
    public class UseRawSql
    {
        /// <summary>
        ///     Include is not working.
        /// </summary>
        public void UseRawSql1()
        {
            using var context = new AnimalContext();

            var foods = context.Food.SqlQuery("select * from Foods where Title = {0}", "Pizza");

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food in foods)
            {
                Console.WriteLine(food);

                Console.Write("\t");
                Console.WriteLine(food.Animal);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is not working.
        /// </summary>
        public void UseRawSql2()
        {
            using var context = new AnimalContext();

            var foods = context.Database.SqlQuery<Food>(
                    "select * from Foods f join Animals a on f.Id = a.CustomIdName")
                //.AsQueryable()
                //.Include(f => f.Animal)
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

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void UseRawSql3()
        {
            using var context = new AnimalContext();
            var       foods   = context.Food.SqlQuery("select * from Foods ");
            var animals = context.Animals
                                 .Include("Food")
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                Console.Write("\t");
                Console.WriteLine(animal.Food);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void UseRawSql4()
        {
            using var context = new AnimalContext();

            var title = new SqlParameter("trees", "'TreesWorshipers'");
            var clubs = context.Clubs.SqlQuery("exec SelectClubsByTitle @Title=@trees", title).AsEnumerable();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var club in clubs)
            {
                Console.WriteLine(club);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}