using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FrameworkLib_Common.Model
{
    //[Table("Beaver")]
    public class Beaver : Animal
    {
        [Required]
        public FluffinessEnum Fluffiness { get; set; }

        public int Size { get; set; }

        public override string ToString()
        {
            return @$"{base.ToString()} Beaver: Fluffiness = {Fluffiness} Size = {Size}";
        }
    }

    public enum FluffinessEnum
    {
        NotFluffy,
        Fluffy,
        VeryFluffy
    }
}