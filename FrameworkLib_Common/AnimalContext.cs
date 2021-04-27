using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Text.RegularExpressions;
using FrameworkLib_Common.Model;

namespace FrameworkLib_Common
{
    public class AnimalContext: DbContext
    {
        #region Tables

        public IDbSet<Person>     Persons      { get; set; } = null!;
        public DbSet<Animal>      Animals      { get; set; } = null!;
        public DbSet<Beaver>      Beavers      { get; set; } = null!;
        public DbSet<Crow>        Crows        { get; set; } = null!;
        public DbSet<Deer>        Deers        { get; set; } = null!;
        public DbSet<Club>        Clubs        { get; set; } = null!;
        public DbSet<Grade>       Grades       => Set<Grade>();
        public DbSet<Job>         Jobs         { get; set; } = null!;
        public DbSet<Drawback>    Drawbacks    { get; set; } = null!;
        public DbSet<JobDrawback> JobDrawbacks { get; set; } = null!;
        public DbSet<Food>        Food         { get; set; } = null!;
        public DbSet<NormalFood>  NormalFood   { get; set; } = null!;
        public DbSet<VeganFood>   VeganFood    { get; set; } = null!;

        public DbSet<MapToQuery> MapToQuery { get; set; } = null!;

        //// Property bags
        //public DbSet<Dictionary<string, object>> Products => Set<Dictionary<string, object>>("Product");
        //public DbSet<Dictionary<string, object>> Categories => Set<Dictionary<string, object>>("Category");

        #endregion

        #region C-ors

        public AnimalContext()
        {
            // NOTE: `true` to enable lazy loading
            // NOTE: when `true` disable for specific property by making it not virtual.
            Configuration.LazyLoadingEnabled = false;
            this.Database.Log                = Console.Write;
        }

        #endregion

        /// <summary>
        ///     Custom table name generation.
        /// </summary>
        private string GetTableName(Type type)
        {
            var pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();

            var result = pluralizationService.Pluralize(type.Name);

            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TPT
            modelBuilder.Entity<Animal>().ToTable("Animals");
            modelBuilder.Entity<Beaver>().ToTable("Beavers");
            modelBuilder.Entity<Crow>().ToTable("Crows");
            modelBuilder.Entity<Deer>().ToTable("Deers");

            // Many-to-many
            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Clubs)
                .WithMany(c => c.Animals);

            //modelBuilder.Entity<Grade>().Property(e => e.TheGrade)
            //        .HasColumnType("decimal(3, 2)");
            // Many-to-many old style
            modelBuilder.Entity<JobDrawback>().HasKey(j => new
            {
                j.JobId,
                j.DrawbackId
            });
            modelBuilder.Entity<JobDrawback>()
                .HasRequired(jd => jd.Job)
                .WithMany(j => j.JobDrawbacks)
                .HasForeignKey(x => x.JobId);
            modelBuilder.Entity<JobDrawback>()
                .HasRequired(jd => jd.Drawback)
                .WithMany(d => d.JobDrawbacks)
                .HasForeignKey(x => x.DrawbackId);

            modelBuilder.Entity<Animal>()
                .HasRequired(t => t.Food)
                .WithRequiredPrincipal(t => t.Animal);

            // Add custom IsUnicode attribute.
            modelBuilder.Properties()
                        .Having(x => x.GetCustomAttributes(false).OfType<IsUnicode>().FirstOrDefault())
                        .Configure((config, att) => config.IsUnicode(att.Unicode));
        }
    }
}
