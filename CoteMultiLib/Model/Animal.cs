using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace CoreMultiLib.Model
{
    //[Table("Animals")]
    [Index(nameof(Name), IsUnique = true)]
    public class Animal
    {
        private Food _food = null!;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)] public string? Name { get; set; }
        public int Age { get; set; }

        public virtual List<Club>?         Clubs   { get; set; }
        public virtual ICollection<Grade>? Grades  { get; set; }
        public virtual Job                 Job     { get; set; } = null!;
        public         int?                JobId   { get; set; }
        public virtual Person?             LovedBy { get; set; }
        public virtual Person?             HatedBy { get; set; }

        [BackingField(nameof(_food))]
        public virtual Food Food
        {
            get => _food;
            set => _food = value;
        }

        // Translates to string in db so Include is not needed.
        public IPAddress IpAddress { get; set; } = null!;

        //public JsonDocument? Passport { get; set; }

        public override string ToString()
        {
            return $"Animal : Id = {Id} Name = {Name} IpAddress = {IpAddress}";
        }
    }
}