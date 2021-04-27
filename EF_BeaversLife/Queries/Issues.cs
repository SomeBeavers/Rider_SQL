using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CoreLib_Common;
using CoreLib_Common.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class Issues
    {
        /// <summary>
        ///     BUG: https://youtrack.jetbrains.com/issue/RSRP-481612
        ///     Extension method with Include is used.
        /// </summary>
        public void RSRP_481612()
        {
            using var context     = new AnimalContext();
            var       animals     = context.Animals.ExtensionMethod();
            var       animalsList = animals.ToImmutableList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animalsList)
            {
                Console.WriteLine(animal);
                if (animal.Clubs != null)
                {
                    var s = animal.Clubs.ToList();
                    foreach (Club club in animal.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     BUG: https://youtrack.jetbrains.com/issue/RSRP-481598
        /// </summary>
        public void RSRP_481598()
        {
            using var            context          = new AnimalContext();
            IQueryable<Drawback> contextDrawbacks = context.Drawbacks.AsQueryable();
            IQueryable<Drawback> drawbacks =
                contextDrawbacks.Include(d => d.Clubs)
                                .ThenInclude(c => c.Animals) ?? contextDrawbacks;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback);
                List<Club> clubs = (drawback.Clubs?.ToList() ?? null)!;

                if (clubs != null)
                {
                    foreach (var club in clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                        if (club.Animals != null)
                        {
                            foreach (var animal in club.Animals)
                            {
                                Console.Write("\t");
                                Console.Write("\t");
                                Console.WriteLine(animal);
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481553
        /// Include is needed.
        /// </summary>
        public static async Task RSRP_481553()
        {
            await using var context = new AnimalContext();
            var crows = context.Crows
                //.Include(c => c.Grades)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            await crows.ForEachAsync(crow =>
            {
                Console.WriteLine(crow);
                if (crow.Grades != null)
                {
                    foreach (var grade in crow.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            });

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481553
        /// Include is needed.
        /// </summary>
        public static void RSRP_481553_Part2()
        {
            using var context = new AnimalContext();
            var drawbacks = context.Drawbacks
                //.Include(d => d.Clubs)
                //.ThenInclude(c => c.Animals)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback);
                drawback.Clubs?.ToList().ForEach(club =>
                {
                    Console.Write("\t");
                    if (club.Animals != null)
                    {
                        Console.WriteLine(club.Animals.Count);
                    }
                });
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481591
        /// Include is NOT needed as variable is reassigned.
        /// </summary>
        public void RSRP_481591()
        {
            using var context = new AnimalContext();
            var       food    = context.Food;
            food = null;

            //food = context.Food;

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (food != null)
            {
                foreach (var food1 in food)
                {
                    Console.WriteLine(food1);
                    if (food1.Drawbacks != null)
                    {
                        foreach (var drawback in food1.Drawbacks)
                        {
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481595
        /// </summary>
        public void RSRP_481595()
        {
            using var context = new AnimalContext();
            var       food    = context.Food ?? null;
            //var       food    = (context.Food ?? null)!;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food1 in food)
            {
                Console.WriteLine(food1);
                List<Drawback> drawbacks = (food1.Drawbacks?.ToList() ?? null)!;

                if (drawbacks != null)
                {
                    foreach (var drawback in drawbacks)
                    {
                        if (drawback.Clubs != null)
                        {
                            foreach (var club in drawback.Clubs)
                            {
                                Console.Write("\t");
                                Console.WriteLine(club);
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481554
        /// </summary>
        public void RSRP_481554()
        {
            using var context     = new AnimalContext();
            using var transaction = context.Database.BeginTransaction();

            Console.ForegroundColor = ConsoleColor.Magenta;

            foreach (var normalFood in context.NormalFood)
            {
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

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481577
        /// </summary>
        public void RSRP_481577()
        {
            using var context = new AnimalContext();
            var beavers = context.Beavers.Include(beaver =>
                beaver.Grades.Where(grade => grade.TheGrade >= 4.0).OrderByDescending(grade => grade.TheGrade).Take(1));

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var beaver in beavers)
            {
                Console.WriteLine(beaver);
                if (beaver.Grades != null)
                {
                    foreach (var grade in beaver.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481581
        /// </summary>
        public void RSRP_481581()
        {
            using var          context    = new AnimalContext();
            IQueryable<Animal> animals    = context.Deers.AsQueryable();
            var                newAnimals = animals;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in newAnimals)
            {
                Console.WriteLine(animal);
                if (animal.Grades != null)
                {
                    foreach (var grade in (animal).Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481581
        /// Include is needed.
        /// </summary>
        public void RSRP_481581_2()
        {
            using var context = new AnimalContext();
            IQueryable<Animal> animals = context.Deers.AsQueryable()
                //.Include(d => d.Elves)
                ;
            var newAnimals = animals;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in newAnimals)
            {
                Console.WriteLine(animal);

                var elves = ((Deer) animal).Elves;
                if (elves != null)
                {
                    foreach (var elf in elves)
                    {
                        Console.Write("\t");
                        Console.WriteLine(elf);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481557
        /// </summary>
        public void RSRP_481557()
        {
            using var context   = new AnimalContext();
            var       drawbacks = context.Drawbacks;
            var       first     = drawbacks.First();
            context.Entry(first).Collection(d => d.Clubs).Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback);
                if (drawback.Clubs != null)
                {
                    foreach (var club in drawback.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);

                        foreach (var grade in club.Grades)
                        {
                            Console.Write("\t");
                            Console.Write("\t");

                            Console.WriteLine(grade);
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481559
        /// </summary>
        public void RSRP_481559()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals;

            // start comment
            var grades = context.Entry(animals.Single(a => a.Id == 1)).Collection(a => a.Grades);
            grades.Load();
            // end comment

            // uncomment
            //context.Entry(animals.Single(a => a.Id == 1)).Collection(a => a.Grades).Query().Load();

            var count = grades.Query().Count();
            Console.WriteLine(count);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                if (animal.Grades != null)
                {
                    foreach (var grade in animal.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481502
        /// </summary>
        public void RSRP_481502(bool boolParameter = false)
        {
            using var     context = new AnimalContext();
            DbSet<Person> persons = context.Persons;

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (boolParameter)
            {
                // uncomment
                //persons.Include(p => p.AnimalsLoved);
                //persons.Include(p => p.AnimalsHated);
            }

            if (boolParameter)
            {
                foreach (var person in persons)
                {
                    Console.WriteLine(person.AnimalsLoved.Count);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481543
        /// </summary>
        public void RSRP_481543()
        {
            using var     context        = new AnimalContext();
            DbSet<Beaver> contextBeavers = context.Beavers;

            var beaversList = contextBeavers.ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var beaver in beaversList)
            {
            }

            for (int i = 0; i < beaversList.Count; i++)
            {
                Console.WriteLine(beaversList[i]);

                for (int j = 0; j < beaversList[i]?.Clubs?.Count; j++)
                {
                    Console.Write("\t");
                    Console.WriteLine(beaversList[i].Clubs[j]);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481545
        /// </summary>
        public void RSRP_481545()
        {
            using var context = new AnimalContext();

            var deers     = context.Deers;
            var deersList = deers.ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var deer in deersList)
            {
                Console.WriteLine(deer);

                if (deer.Clubs != null)
                {
                    foreach (var club in deer.Clubs)
                    {
                        if (club.Drawbacks != null)
                        {
                            foreach (var drawback in club.Drawbacks)
                            {
                                if (drawback.Foods != null)
                                {
                                    foreach (var food in drawback.Foods)
                                    {
                                        Console.WriteLine(food);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481514
        /// </summary>
        public void RSRP_481514()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals.Include(a => a.Grades).ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            var gradesCount = animals[0].Grades?.Count;

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481519
        /// </summary>
        public void RSRP_481519()
        {
            using var context = new AnimalContext();
            var drawbacks = context.Drawbacks.Include(d => d.Clubs)
                                   .ThenInclude(c => c.Animals)
                                   .ThenInclude(a => a.Food)
                                   .ThenInclude(f => f.Drawbacks);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                try
                {
                    var foodDrawbacks = drawback?.Clubs?.First(club => club.Id        > 2)
                                                .Animals?.First(animal => animal.Food != null)
                                                .Food?.Drawbacks;
                    if (foodDrawbacks != null)
                    {
                        foreach (var foodDrawback in foodDrawbacks)
                        {
                            Console.WriteLine(foodDrawback.JobDrawbacks);
                            Console.WriteLine(foodDrawback.Title);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481512
        /// </summary>
        public void RSRP_481512()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals.Where(a => a.Grades.Count(x => x.TheGrade > 3) > 0);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);

                if (animal.Grades != null)
                {
                    foreach (var grade in animal.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481544
        /// </summary>
        public void RSRP_481544()
        {
            using var context = new AnimalContext();
            var beavers = context.Beavers
                                 .Include(b => b.Clubs).ThenInclude(c => c.Animals)
                                 .Include(b => b.Clubs).ThenInclude(g => g.Drawbacks);
            var localBeavers = beavers.ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var beaver in localBeavers)
            {
                Console.WriteLine(beaver);

                if (beaver.Clubs != null)
                {
                    foreach (var club in beaver.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);

                        foreach (var grade in club.Grades)
                        {
                            Console.Write("\t");
                            Console.Write("\t");
                            Console.WriteLine(grade);

                            if (grade.Animal.Grades != null)
                            {
                                foreach (var animalGrade in grade.Animal.Grades)
                                {
                                    Console.Write("\t");
                                    Console.Write("\t");
                                    Console.Write("\t");

                                    Console.WriteLine(animalGrade);
                                }
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481565
        /// </summary>
        public void RSRP_481565()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                foreach (var club in animal.Clubs)
                {
                    foreach (var drawback in club.Drawbacks)
                    {
                        foreach (var food in drawback.Foods)
                        {
                        }

                        foreach (var drawbackClub in drawback.Clubs)
                        {
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481721
        /// </summary>
        public void RSRP_481721()
        {
            using (var context = new AnimalContext())
            {
                var clubs     = context.Clubs;
                var firstClub = clubs.First();
                context.Entry(firstClub).Collection(c => c.Drawbacks).Load();
            }

            using (var context = new AnimalContext())
            {
                var drawbacks = context.Drawbacks.Include(d => d.JobDrawbacks);
                Console.ForegroundColor = ConsoleColor.Magenta;
                foreach (var drawback in drawbacks)
                {
                    Console.WriteLine(drawback);

                    foreach (JobDrawback jobDrawback in drawback.JobDrawbacks)
                    {
                        Console.Write("\t");
                        Console.WriteLine(jobDrawback);
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481722
        /// </summary>
        public void RSRP_481722()
        {
            using var context = new AnimalContext();
            var foods = context.Food
                               .Include(f => f.Drawbacks)
                               .ThenInclude(d => d.Clubs)
                               .ThenInclude(c => c.Animals)
                               .ThenInclude(g => g.Grades)
                               .Include(f => f.Drawbacks)
                //.ThenInclude(d => d.Clubs)
                //.ThenInclude(c => c.AdditionalInfos)
                ;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var food in foods)
            {
                foreach (var drawback in food.Drawbacks)
                {
                    foreach (var club in drawback.Clubs)
                    {
                        foreach (var animal in club.Animals)
                        {
                            foreach (var grade in animal.Grades)
                            {
                                Console.WriteLine(grade);

                                foreach (var club1 in grade.Animal.Clubs)
                                {
                                    if (club1.AdditionalInfos != null)
                                    {
                                        foreach (var additionalInfo in club1.AdditionalInfos)
                                        {
                                            Console.Write("\t");
                                            Console.WriteLine(additionalInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481520
        /// </summary>
        public void RSRP_481520(AnimalContext context)
        {
            var animals = context.Animals;
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                var drawbacks = animal.Food?.Drawbacks;

                if (drawbacks != null)
                {
                    foreach (var drawback in drawbacks)
                    {
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481734
        /// </summary>
        public void RSRP_481734(AnimalContext context, DbSet<Club> clubs)
        {
            var contextClubs = clubs;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var club in contextClubs)
            {
                Console.WriteLine(club);
                if (club.Drawbacks != null)
                {
                    foreach (var drawback in club.Drawbacks)
                    {
                        if (drawback.Foods != null)
                        {
                            foreach (var food in drawback.Foods)
                            {
                                Console.Write("\t");
                                Console.WriteLine(food);
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481736
        /// </summary>
        public void RSRP_481736(AnimalContext context, DbSet<Club> clubs)
        {
            var animals = context?.Animals.Include($"Clubs");

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                foreach (var club in animal.Clubs)
                {
                    foreach (var grade in club.Grades)
                    {
                        Console.Write("\t");
                        Console.WriteLine(grade);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481531
        /// </summary>
        public async Task RSRP_481531()
        {
            using var context = new AnimalContext();

            Console.ForegroundColor = ConsoleColor.Magenta;
            await foreach (var drawback in context.Drawbacks)
            {
                Console.WriteLine(drawback);
                if (drawback.Clubs != null)
                {
                    foreach (var club in drawback.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                    }
                }
            }

            await foreach (var animal in context.Animals.AsAsyncEnumerable())
            {
                foreach (var grade in animal.Grades)
                {
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481516
        /// </summary>
        public void RSRP_481516()
        {
            using var context = new AnimalContext();
            var       animal  = context.Animals.FirstOrDefault();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var club in animal.Clubs)
            {
                Console.WriteLine(club);
                foreach (var drawback in club.Drawbacks)
                {
                    Console.Write("\t");
                    Console.WriteLine(drawback);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481516
        /// </summary>
        public static void RSRP_481516_2()
        {
            using var context = new AnimalContext();
            var       animals = context.Animals.ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                foreach (var club in animal.Clubs)
                {
                    foreach (var drawback in club.Drawbacks)
                    {
                        Console.Write("\t");
                        Console.WriteLine(drawback);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481516
        /// </summary>
        public static async Task RSRP_481516_3(AnimalContext context)
        {
            var additionalInfos = await context.AdditionalInfos.ToListAsync();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var additionalInfo in additionalInfos)
            {
                Console.WriteLine(additionalInfo);
                foreach (var club in additionalInfo.Clubs)
                {
                    Console.Write("\t");
                    Console.WriteLine(club);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481578
        /// </summary>
        public void RSRP_481578()
        {
            using var          context = new AnimalContext();
            IQueryable<Animal> animals = context.Animals;
            animals = context.Animals.Include(a => a.Grades);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);

                foreach (var grade in animal.Grades)
                {
                    Console.Write("\t");
                    Console.WriteLine(grade);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481538
        /// </summary>
        public void RSRP_481538()
        {
            using var context = new AnimalContext();
            var       clubs   = context.Clubs;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var club in context.Clubs)
            {
                Console.WriteLine(club);

                foreach (var location in club.Locations)
                {
                    Console.Write("\t");
                    Console.WriteLine(location);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481644
        /// </summary>
        public void RSRP_481644()
        {
            using var context = new AnimalContext();

            var animals = context.Animals;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.Job.JobDrawbacks.FirstOrDefault()?.Drawback.Clubs.FirstOrDefault()?.Grades
                                        .Count);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481701
        /// </summary>
        public void RSRP_481701()
        {
            using var context = new AnimalContext();
            var drawbacks = context.Drawbacks.Include(item => item.Clubs).ThenInclude(item => item.Animals)
                                   .ThenInclude(item => item.Grades);


            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                Console.WriteLine(drawback?.Clubs?.FirstOrDefault()?.Animals?.FirstOrDefault()?.Grades?.Count);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481645
        /// </summary>
        public void RSRP_481645()
        {
            using var context   = new AnimalContext();
            var       drawbacks = context.Drawbacks;

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in drawbacks)
            {
                try
                {
                    var animalsHated = drawback?.Clubs?.First(c => c.Locations.Count > 1)
                                               .Animals?.First(a => a.LovedBy        != null)
                                               .LovedBy?.AnimalsHated;
                    foreach (var animal in animalsHated)
                    {
                        Console.WriteLine(animal);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public delegate IQueryable<Club> AddIncludeDelegate(IQueryable<Club> value);

        public IQueryable<Club> clubsField;

        /// <summary>
        /// BUG: https://youtrack.jetbrains.com/issue/RSRP-481532
        /// </summary>
        public void RSRP_481532(AnimalContext context)
        {
            clubsField = context.Clubs;

            AddIncludeDelegate addInclude = delegate(IQueryable<Club> val)
            {
                return val.Include(item => item.Animals);
            };

            clubsField = addInclude(clubsField);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var club in clubsField)
            {
                for (var i = 0; i < club.Animals.Count; i++)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static class Ext
    {
        public static IQueryable<Animal> ExtensionMethod(this DbSet<Animal> a)
        {
            return a.Include(item => item.Clubs);
        }
    }
}