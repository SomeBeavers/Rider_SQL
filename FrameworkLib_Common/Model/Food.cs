using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FrameworkLib_Common.Model
{
    public class Food
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public         string                 Title     { get; set; } = null!;
        public virtual Animal?                Animal    { get; set; } = null!;
        public         int?                   AnimalId  { get; set; }
        public virtual ICollection<Drawback>? Drawbacks { get; set; }

        public override string ToString()
        {
            return $"Food : Id = {Id} Title = {Title}";
        }
    }

    public class NormalFood : Food
    {
        [Required]
        public Taste Taste { get; set; }

        public override string ToString()
        {
            return @$"{base.ToString()} NormalFood : Taste = {Taste}";
        }
    }

    public enum Taste
    {
        Excellent,
        VeryGood,
        Good,
        Normal,
        Bad,
        VeryBad,
        Dirt
    }

    public class VeganFood : Food
    {
        public int Calories { get; set; }

        public override string ToString()
        {
            return @$"{base.ToString()} VeganFood : Calories = {Calories}";
        }
    }
}