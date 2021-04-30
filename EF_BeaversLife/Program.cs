#region test

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreLib_Common;
using CoreLib_Common.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#endregion

namespace EF_BeaversLife
{
    // NOTE [for me]: use ef_method template to generate simple ef method
    // NOTE [for me]: use print_format to paste Console.Write("\t");
    internal class Program
    {
        private static async Task Main()
        {
            // SeedDb();

            Console.ForegroundColor = ConsoleColor.Green;
            ExecuteQueries();
            await ExecuteQueriesAsync();

            Console.ForegroundColor = ConsoleColor.White;

            using var context = new AnimalContext();

            //context.Database.EnsureDeleted();
        }

        private static void ExecuteQueries()
        {
            using var context = new AnimalContext();

            // replace start
            string pizza = "Pizza";
            var foods2 = context.Food.FromSqlInterpolated($"select * from Food where Title = {pizza}")
                                .Include(food => food.Animal);
            
            // replace end

            Console.ForegroundColor = ConsoleColor.Magenta;
            // replace start
            foreach (var food in foods2)
            {
                Console.WriteLine(food);
            }
            // replace end

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void AllQueries()
        {
            using var context = new AnimalContext();
            var foods = context.Food.FromSqlRaw("select * from Food where Title = {0}", "Pizza")
                               .Include(food => food.Animal);

            string pizza = "Pizza";
            var foods2 = context.Food.FromSqlInterpolated($"select * from Food where Title = {pizza}")
                                .Include(food => food.Animal);

            var pizza2 = new SqlParameter("parameter", "Pizza");
            var foods3 = context.Food.FromSqlRaw("select * from Food where Title = @parameter", pizza2)
                                .Include(food => food.Animal);

            var title = new SqlParameter("trees", "'TreesWorshipers'");
            var clubs = context.Clubs.FromSqlRaw("exec SelectClubsByTitle @Title=@trees", title).AsEnumerable();
            
            var pizza4                            = new SqlParameter("parameter", "Pizza");
            var queryString = "select * from Food where Title = @parameter";
            // var foods4 = context.Food.FromSqlRaw(queryString, pizza4)
            //                     .Include(food => food.Animal);
            
            string longQuery1 = "select j.Salary, j.Title as JobTitle, dr.Title as DrawbackTitle, dr.Consequence_Name from Jobs j"+
            "inner join JobDrawbacks jd"+
            "on j.Id = jd.JobId"+
            "inner join Drawbacks dr"+
            "on jd.DrawbackId = dr.Id"+
            "order by j.Salary";
            string concatenation   = "select Id, Title from Drawbacks where Title = " + "Test";
            string test            = "Test";
            string interpolaction  = $"select Id, Title from Drawbacks where Title = {test}";
            string verbatimString  = @"select Id, Title from Drawbacks where Title = " + "Test";
            string withPlaceholder = @"select Id, Title from Drawbacks where Title = {0}";
            string withPlaceholder2 = string.Format("select Id, Title from Drawbacks where Title = '{0}'", 1);
            string withPlaceholder3 = string.Format("select Id, Title from Drawbacks where Title = {0}", 1);
        }

        private static async Task ExecuteQueriesAsync()
        {
            //await new UseLinq().UseLinq1();
        }

        private static void SeedDb()
        {
            using var context = new AnimalContext();

            context.SavedChanges += (sender, args) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"Saved {args.EntitiesSavedCount} changes for {((DbContext) sender)?.Database.GetConnectionString()}");
                Console.ForegroundColor = ConsoleColor.White;
            };

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            #region Seed TPT (Animal)

            var beaver1 = new Beaver
            {
                Name       = "SomeBeavers1",
                Age        = 27,
                Fluffiness = FluffinessEnum.VeryFluffy,
                Size       = 15,
                IpAddress  = IPAddress.Parse("127.0.0.1")
            };
            var beaver2 = new Beaver
            {
                Name       = "SomeBeavers2",
                Age        = 26,
                Fluffiness = FluffinessEnum.Fluffy,
                Size       = 14,
                IpAddress  = IPAddress.Parse("127.0.0.1")
            };
            var beaver3 = new Beaver
            {
                Name       = "SomeBeavers3",
                Age        = 25,
                Fluffiness = FluffinessEnum.NotFluffy,
                Size       = 13,
                IpAddress  = IPAddress.Parse("127.0.0.1")
            };
            var beaver4 = new Beaver
            {
                Name       = "SomeBeavers4",
                Age        = 24,
                Fluffiness = FluffinessEnum.Fluffy,
                Size       = 12,
                IpAddress  = IPAddress.Parse("127.0.0.1")
            };
            var beaver5 = new Beaver
            {
                Name       = "SomeBeavers5",
                Age        = 23,
                Fluffiness = FluffinessEnum.VeryFluffy,
                Size       = 11,
                IpAddress  = IPAddress.Parse("127.0.0.1")
            };

            var crow1 = new Crow
            {
                Name      = "Crowly",
                Age       = 5,
                Color     = "black",
                Size      = 1,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var crow2 = new Crow
            {
                Name      = "Crowly1",
                Age       = 5,
                Color     = "black",
                Size      = 1,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var crow3 = new Crow
            {
                Name      = "Crowly2",
                Age       = 22,
                Color     = "black",
                Size      = 4,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var crow4 = new Crow
            {
                Name      = "Crowly3",
                Age       = 50,
                Color     = "white",
                Size      = 10,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var crow5 = new Crow
            {
                Name      = "Crowly4",
                Age       = 5,
                Color     = "pink",
                Size      = 1,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };

            var deer1 = new Deer
            {
                Name      = "Dasher",
                Age       = 1,
                Horns     = true,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer2 = new Deer
            {
                Name      = "Dancer",
                Age       = 2,
                Horns     = true,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer3 = new Deer
            {
                Name      = "Prancer",
                Age       = 1,
                Horns     = false,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer4 = new Deer
            {
                Name      = "Vixen",
                Age       = 1,
                Horns     = true,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer5 = new Deer
            {
                Name      = "Comet",
                Age       = 1,
                Horns     = true,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer6 = new Deer
            {
                Name      = "Cupid",
                Age       = 1,
                Horns     = false,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer7 = new Deer
            {
                Name      = "Donder ",
                Age       = 1,
                Horns     = true,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };
            var deer8 = new Deer
            {
                Name      = "Blitzen",
                Age       = 1,
                Horns     = true,
                IpAddress = IPAddress.Parse("127.0.0.1")
            };

            context.Beavers.Add(beaver1);
            context.Beavers.Add(beaver2);
            context.Beavers.Add(beaver3);
            context.Beavers.Add(beaver4);
            context.Beavers.Add(beaver5);

            context.Crows.Add(crow1);
            context.Crows.Add(crow2);
            context.Crows.Add(crow3);
            context.Crows.Add(crow4);
            context.Crows.Add(crow5);

            context.Deers.Add(deer1);
            context.Deers.Add(deer2);
            context.Deers.Add(deer3);
            context.Deers.Add(deer4);
            context.Deers.Add(deer5);
            context.Deers.Add(deer6);
            context.Deers.Add(deer7);
            context.Deers.Add(deer8);

            #endregion

            #region Seed Many-to-many (Club)

            var club1 = new Club
            {
                Title   = "TreesWorshipers",
                Animals = new List<Animal> {beaver1, beaver2, beaver3, beaver4, beaver5, crow4},
                Locations = new List<Location>
                {
                    new()
                    {
                        Address = "North America"
                    },
                    new()
                    {
                        Address = "Canada"
                    },
                    new()
                    {
                        Address = "Russia"
                    }
                }
            };

            var club2 = new Club
            {
                Title   = "CornLovers",
                Animals = new List<Animal> {crow1, crow2, crow3, crow4, crow5},
                Locations = new List<Location>
                {
                    new()
                    {
                        Address = "Westeros"
                    }
                }
            };

            var club3 = new Club
            {
                Title = "ChristmasTeam",
                Animals = new List<Animal>
                {
                    beaver1, beaver2, beaver3, beaver4, beaver5,
                    crow1, crow2, crow3, crow4, crow5,
                    deer1, deer2, deer3, deer4, deer5, deer6, deer7, deer8
                },
                Locations = new List<Location>
                {
                    new()
                    {
                        Address = "North Pole"
                    }
                }
            };

            context.Clubs.Add(club1);
            context.Clubs.Add(club2);
            context.Clubs.Add(club3);

            #endregion

            #region Seed Grades

            var grade1 = new Grade
            {
                TheGrade = 5,
                Club     = club1,
                Animal   = beaver1
            };
            var grade2 = new Grade
            {
                TheGrade = 4,
                Club     = club1,
                Animal   = beaver2
            };
            var grade3 = new Grade
            {
                TheGrade = 3,
                Club     = club1,
                Animal   = beaver3
            };
            var grade4 = new Grade
            {
                TheGrade = 3,
                Club     = club1,
                Animal   = beaver4
            };
            var grade5 = new Grade
            {
                TheGrade = 2,
                Club     = club1,
                Animal   = beaver5
            };
            var grade6 = new Grade
            {
                TheGrade = 1,
                Club     = club1,
                Animal   = crow4
            };
            var grade7 = new Grade
            {
                TheGrade = 5,
                Club     = club2,
                Animal   = crow1
            };
            var grade8 = new Grade
            {
                TheGrade = 4.5,
                Club     = club2,
                Animal   = crow2
            };
            var grade9 = new Grade
            {
                TheGrade = 2.1,
                Club     = club2,
                Animal   = crow3
            };
            var grade10 = new Grade
            {
                TheGrade = 4.3,
                Club     = club2,
                Animal   = crow4
            };

            var grade27 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = beaver1
            };
            var grade26 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = beaver2
            };
            var grade25 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = beaver3
            };
            var grade24 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = beaver4
            };
            var grade23 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = beaver5
            };
            var grade22 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = crow1
            };
            var grade21 = new Grade
            {
                TheGrade = 3.5,
                Club     = club3,
                Animal   = crow2
            };
            var grade20 = new Grade
            {
                TheGrade = 2.5,
                Club     = club3,
                Animal   = crow3
            };
            var grade19 = new Grade
            {
                TheGrade = 1.5,
                Club     = club3,
                Animal   = crow4
            };
            var grade28 = new Grade
            {
                TheGrade = 4.9,
                Club     = club3,
                Animal   = crow5
            };
            var grade11 = new Grade
            {
                TheGrade = 4.8,
                Club     = club3,
                Animal   = deer1
            };
            var grade12 = new Grade
            {
                TheGrade = 4.7,
                Club     = club3,
                Animal   = deer2
            };
            var grade13 = new Grade
            {
                TheGrade = 4.6,
                Club     = club3,
                Animal   = deer3
            };
            var grade14 = new Grade
            {
                TheGrade = 4.5,
                Club     = club3,
                Animal   = deer4
            };
            var grade15 = new Grade
            {
                TheGrade = 4.4,
                Club     = club3,
                Animal   = deer5
            };
            var grade16 = new Grade
            {
                TheGrade = 4.3,
                Club     = club3,
                Animal   = deer6
            };
            var grade17 = new Grade
            {
                TheGrade = 4.2,
                Club     = club3,
                Animal   = deer7
            };
            var grade18 = new Grade
            {
                TheGrade = 4.1,
                Club     = club3,
                Animal   = deer8
            };

            context.Grades.Add(grade1);
            context.Grades.Add(grade2);
            context.Grades.Add(grade3);
            context.Grades.Add(grade4);
            context.Grades.Add(grade5);
            context.Grades.Add(grade6);
            context.Grades.Add(grade7);
            context.Grades.Add(grade8);
            context.Grades.Add(grade9);
            context.Grades.Add(grade10);
            context.Grades.Add(grade11);
            context.Grades.Add(grade12);
            context.Grades.Add(grade13);
            context.Grades.Add(grade14);
            context.Grades.Add(grade15);
            context.Grades.Add(grade16);
            context.Grades.Add(grade17);
            context.Grades.Add(grade18);
            context.Grades.Add(grade19);
            context.Grades.Add(grade20);
            context.Grades.Add(grade21);
            context.Grades.Add(grade22);
            context.Grades.Add(grade23);
            context.Grades.Add(grade24);
            context.Grades.Add(grade25);
            context.Grades.Add(grade26);
            context.Grades.Add(grade27);
            context.Grades.Add(grade28);

            #endregion

            #region Seed Jobs

            var job1 = new Job
            {
                Title  = "Builder",
                Salary = 1,
                Animals = new List<Animal>
                {
                    beaver1, beaver2, beaver3, beaver4, beaver5
                }
            };
            var job2 = new Job
            {
                Title  = "Messenger",
                Salary = 10,
                Animals = new List<Animal>
                {
                    crow1, crow2, crow3, crow4
                }
            };
            var job3 = new Job
            {
                Title  = "Delivery",
                Salary = 100,
                Animals = new List<Animal>
                {
                    deer1, deer2, deer3, deer4, deer5, deer6, deer7, deer8
                }
            };

            context.Jobs.Add(job1);
            context.Jobs.Add(job2);
            context.Jobs.Add(job3);

            #endregion

            #region Seed TPH (Food)

            var food1 = new NormalFood
            {
                Title  = "Elm",
                Animal = beaver1,
                Taste  = Taste.Normal
            };
            var food2 = new VeganFood
            {
                Title    = "Daphne laureola",
                Animal   = beaver2,
                Calories = 100
            };
            var food3 = new VeganFood
            {
                Title    = "Carpinus betulus",
                Animal   = beaver3,
                Calories = 1001
            };
            var food4 = new VeganFood
            {
                Title    = "Hornbeam",
                Animal   = beaver4,
                Calories = 101
            };
            var food5 = new NormalFood
            {
                Title  = "Pizza",
                Animal = beaver5,
                Taste  = Taste.Excellent
            };
            var food6 = new NormalFood
            {
                Title  = "Steak",
                Animal = crow1,
                Taste  = Taste.Excellent
            };
            var food7 = new NormalFood
            {
                Title  = "Meat",
                Animal = crow2,
                Taste  = Taste.Good
            };
            var food8 = new NormalFood
            {
                Title  = "Pizza",
                Animal = crow3,
                Taste  = Taste.VeryGood
            };
            var food9 = new VeganFood
            {
                Title    = "Corn",
                Animal   = crow4,
                Calories = 1
            };
            var food10 = new NormalFood
            {
                Title  = "Pizza",
                Animal = crow5,
                Taste  = Taste.Normal
            };
            var food11 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer1,
                Calories = 10
            };
            var food12 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer2,
                Calories = 10
            };
            var food13 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer3,
                Calories = 10
            };
            var food14 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer4,
                Calories = 10
            };
            var food15 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer5,
                Calories = 10
            };
            var food16 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer6,
                Calories = 10
            };
            var food17 = new NormalFood
            {
                Title  = "Elves",
                Animal = deer7,
                Taste  = Taste.Excellent
            };
            var food18 = new VeganFood
            {
                Title    = "Pizza",
                Animal   = deer8,
                Calories = 10
            };

            context.Food.Add(food1);
            context.Food.Add(food2);
            context.Food.Add(food3);
            context.Food.Add(food4);
            context.Food.Add(food5);
            context.Food.Add(food6);
            context.Food.Add(food7);
            context.Food.Add(food8);
            context.Food.Add(food9);
            context.Food.Add(food10);
            context.Food.Add(food11);
            context.Food.Add(food12);
            context.Food.Add(food13);
            context.Food.Add(food14);
            context.Food.Add(food15);
            context.Food.Add(food16);
            context.Food.Add(food17);
            context.Food.Add(food18);

            #endregion

            #region Seed Many-to-many old style (Drawback)

            var drawback1 = new Drawback
            {
                Title = "Crowdy",
                Foods = new List<Food>
                {
                    food1, food2, food3, food4, /*food5,*/ food6, food7, food8, food9, food10, food11, food12, food13,
                    food14, food15, food16, food17, food18
                },
                Clubs = new List<Club>
                {
                    club1, club2, club3
                },
                Consequence = new Consequence
                {
                    Name = "Nervousness"
                }
            };
            var drawback2 = new Drawback
            {
                Title = "Windy",
                Foods = new List<Food>
                {
                    food1, food2, food3, food4, food5, food6, food7, food8, food9, food10, food11, food12, food13,
                    food14, food15, food16, food17, food18
                },
                Clubs = new List<Club>
                {
                    club1, club2, club3
                },
                Consequence = new Consequence
                {
                    Name = "Teleportation to Land of Oz"
                }
            };
            var drawback3 = new Drawback
            {
                Title = "Soggy",
                Foods = new List<Food>
                {
                    food1, food2, food3, food4, food5, food6, food7, food8, food9, food10, food11, food12, food13,
                    food14, food15, food16, food17, food18
                },
                Clubs = new List<Club>
                {
                    club1, club2, club3
                },
                Consequence = new Consequence
                {
                    Name = "Wet clothes"
                }
            };
            var drawback4 = new Drawback
            {
                Title = "Hardy",
                Foods = new List<Food>
                {
                    food1, food2, food3, food4, food5, food6, food7, food8, food9, food10, food11, food12, food13,
                    food14, food15, food16, food17, food18
                },
                Clubs = new List<Club>
                {
                    club1, club2, club3
                },
                Consequence = new Consequence
                {
                    Name = "Sadness"
                }
            };

            context.Drawbacks.Add(drawback1);
            context.Drawbacks.Add(drawback2);
            context.Drawbacks.Add(drawback3);
            context.Drawbacks.Add(drawback4);

            var jobDrawback1 = new JobDrawback
            {
                Job      = job1,
                Drawback = drawback1
            };
            var jobDrawback2 = new JobDrawback
            {
                Job      = job1,
                Drawback = drawback2
            };
            var jobDrawback3 = new JobDrawback
            {
                Job      = job1,
                Drawback = drawback3
            };
            var jobDrawback4 = new JobDrawback
            {
                Job      = job1,
                Drawback = drawback4
            };
            var jobDrawback5 = new JobDrawback
            {
                Job      = job2,
                Drawback = drawback1
            };
            var jobDrawback6 = new JobDrawback
            {
                Job      = job2,
                Drawback = drawback2
            };
            var jobDrawback7 = new JobDrawback
            {
                Job      = job3,
                Drawback = drawback1
            };
            var jobDrawback8 = new JobDrawback
            {
                Job      = job3,
                Drawback = drawback2
            };

            context.JobDrawbacks.Add(jobDrawback1);
            context.JobDrawbacks.Add(jobDrawback2);
            context.JobDrawbacks.Add(jobDrawback3);
            context.JobDrawbacks.Add(jobDrawback4);
            context.JobDrawbacks.Add(jobDrawback5);
            context.JobDrawbacks.Add(jobDrawback6);
            context.JobDrawbacks.Add(jobDrawback7);
            context.JobDrawbacks.Add(jobDrawback8);

            #endregion

            context.SaveChanges();

            #region Seed Property Bags (Category, Product)

            var category1 = new Dictionary<string, object>
            {
                ["Name"]   = "Beverages",
                ["FoodId"] = food1.Id
            };

            context.Categories.Add(category1);

            context.SaveChanges();

            var product1 = new Dictionary<string, object>
            {
                ["Name"]       = "Product1",
                ["CategoryId"] = context.Categories.First()["Id"]
            };

            context.Products.Add(product1);

            #endregion

            #region Seed Persons

            var person1 = new Person
            {
                Name         = "BeaverPerson",
                AnimalsLoved = new List<Animal> {beaver1, beaver2, beaver3, beaver4, beaver5},
                AnimalsHated = new List<Animal> {deer1, deer2, deer3, deer4, deer5, deer6, deer7, deer8}
            };

            context.Persons.Add(person1);

            #endregion

            #region Seed Elves

            var elf1 = new Elf
            {
                Name = "Alabaster Snowball",
                Deer = deer1
            };
            var elf2 = new Elf
            {
                Name = "Bushy Evergreen",
                Deer = deer1
            };
            var elf3 = new Elf
            {
                Name = "Pepper Minstix",
                Deer = deer2
            };
            var elf4 = new Elf
            {
                Name = "Shinny Upatree",
                Deer = deer3
            };
            var elf5 = new Elf
            {
                Name = "Sugarplum Mary",
                Deer = deer4
            };
            var elf6 = new Elf
            {
                Name = "Wunorse Openslae",
                Deer = deer5
            };
            var elf7 = new Elf
            {
                Name = "Grinch",
                Deer = deer6
            };
            var elf8 = new Elf
            {
                Name = "Legolas",
                Deer = deer7
            };
            var elf9 = new Elf
            {
                Name = "Iorveth",
                Deer = deer8
            };

            context.Elves.Add(elf1);
            context.Elves.Add(elf2);
            context.Elves.Add(elf3);
            context.Elves.Add(elf4);
            context.Elves.Add(elf5);
            context.Elves.Add(elf6);
            context.Elves.Add(elf7);
            context.Elves.Add(elf8);
            context.Elves.Add(elf9);

            #endregion

            #region Seed AdditionaInfos

            var additionalInfo1 = new AdditionalInfo()
            {
                Clubs   = new List<Club>() {club1, club2, club3},
                Comment = "Best club ever"
            };
            var additionalInfo2 = new AdditionalInfo()
            {
                Clubs   = new List<Club>() {club1, club2},
                Comment = "Evolution club"
            };
            var additionalInfo3 = new AdditionalInfo()
            {
                Clubs   = new List<Club>() {club1},
                Comment = "Original club"
            };

            context.AdditionalInfos.Add(additionalInfo1);
            context.AdditionalInfos.Add(additionalInfo2);
            context.AdditionalInfos.Add(additionalInfo3);

            #endregion

            context.SaveChanges();

            context.Database.ExecuteSqlRaw(File.ReadAllText(".\\BD\\CreateTVF.sql"));
        }
    }
}