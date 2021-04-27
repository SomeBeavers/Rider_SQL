using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CoreLib_Common.Model
{
    public class Elf
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual Deer? Deer { get; set; }

        public override string ToString()
        {
            return $"Elf : Id = {Id} Name = {Name}";
        }
    }
}