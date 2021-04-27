using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreLib_Common.Model
{
    //[Table("Deer")]
    public class Deer : Animal
    {
        public bool Horns { get; set; }

        public virtual ICollection<Elf>? Elves { get; set; }

        public override string ToString()
        {
            return @$"{base.ToString()} Deer : Horns = {this.Horns}";
        }
    }
}