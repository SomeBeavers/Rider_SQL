using System;
using System.Linq;
using CoreMultiLib;
using CoreMultiLib.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Queries
{
    public class UseEnumeration
    {
        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToToArray()
        {
            using var context = new AnimalContext();
            var jobs = context.Jobs
                              .Include(job => job.JobDrawbacks)
                              .ThenInclude(jd => jd.Drawback)
                              .ToArray();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(jobs[0].JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToToDictionary()
        {
            using var context = new AnimalContext();
            var jobs = context.Jobs
                              .Include(job => job.JobDrawbacks)
                              .ThenInclude(jd => jd.Drawback)
                              .ToDictionary(p => p.Id);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(jobs[1].JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToToList()
        {
            using var context = new AnimalContext();
            var jobs = context.Jobs
                              .Include(job => job.JobDrawbacks)
                              .ThenInclude(jd => jd.Drawback)
                              .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(jobs[0].JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include of Drawbacks is NOT needed cause it is already loaded in current context.
        /// </summary>
        public void DbCallDueToToList2()
        {
            using var context   = new AnimalContext();
            var       drawbacks = context.Drawbacks.ToList();

            var jobs = context.Jobs
                              .Include(job => job.JobDrawbacks)
                              .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(jobs[0].JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include of Drawbacks is needed cause it is loaded in different context.
        /// </summary>
        public void DbCallDueToToList3()
        {
            using var context   = new AnimalContext();
            using var context2  = new AnimalContext();
            var       drawbacks = context2.Drawbacks.ToList();

            var jobs = context.Jobs
                              .Include(job => job.JobDrawbacks)
                              .ThenInclude(jd => jd.Drawback)
                              .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(jobs[0].JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToFirst()
        {
            using var context = new AnimalContext();
            var job = context.Jobs
                             .Include(j => j.JobDrawbacks)
                             .ThenInclude(jd => jd.Drawback)
                             .First();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(job.JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToAny()
        {
            using var context = new AnimalContext();

            Console.ForegroundColor = ConsoleColor.Magenta;
            var jobs = context.Jobs
                              .Include(j => j.JobDrawbacks)
                              .ThenInclude(jd => jd.Drawback)
                ;
            if (jobs.Any())
            {
                Console.WriteLine(jobs.First().JobDrawbacks?.FirstOrDefault()?.Drawback);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is NOT needed.
        /// </summary>
        public void DbCallDueToLoad()
        {
            using var context = new AnimalContext();

            Console.ForegroundColor = ConsoleColor.Magenta;
            context.Jobs.Load();
            context.Drawbacks.Load();
            context.JobDrawbacks.Load();

            Console.WriteLine(context.Jobs.First().JobDrawbacks?.FirstOrDefault()?.Drawback);

            foreach (var job in context.Jobs)
            {
                Console.WriteLine(job);
                Console.Write("\t");
                Console.WriteLine(job.JobDrawbacks?.FirstOrDefault()?.Drawback);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToReload()
        {
            using var context = new AnimalContext();

            Console.ForegroundColor = ConsoleColor.Magenta;

            var job = context.Jobs
                             .Include(j => j.JobDrawbacks)
                             .ThenInclude(jd => jd.Drawback)
                             .First();
            job.Salary = 0;
            context.Entry(job).Reload();

            Console.WriteLine(job.JobDrawbacks?.FirstOrDefault()?.Drawback);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is NOT needed.
        /// </summary>
        public void DbCallDueToFindAndLoad()
        {
            using var context = new AnimalContext();
            var       animal  = context.Animals.Find(1);
            context.Entry(animal).Collection(a => a.Grades).Query().Where(grade => grade.TheGrade > 4.0).Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(animal);
            if (animal is {Grades: { }})
            {
                foreach (var grade in animal.Grades)
                {
                    Console.Write("\t");
                    Console.WriteLine(grade);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Include is needed.
        /// </summary>
        public void DbCallDueToLoad2()
        {
            using var context = new AnimalContext();
            context.Drawbacks
                   .Include(d => d.Clubs)
                   .Load();

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var drawback in context.ChangeTracker.Entries<IDrawback>())
            {
                Console.WriteLine(drawback.Entity);

                if (drawback.Entity.Clubs != null)
                {
                    foreach (var club in drawback.Entity.Clubs)
                    {
                        Console.Write("\t");
                        Console.WriteLine(club);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}