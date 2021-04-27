using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FrameworkLib_Common.Model
{
    public class Drawback : IDrawback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public         string                    Title          { get; set; } = null!;
        public virtual ICollection<JobDrawback>? JobDrawbacks   { get; set; }
        public virtual ICollection<Food>?        Foods          { get; set; }
        public virtual ICollection<Club>?        Clubs          { get; set; }
        public virtual Consequence               Consequence    { get; set; } = null!;
        public         DrawbackDetails           DrawbackDetail { get; set; } = null!;

        public override string ToString()
        {
            return $"Drawback : Id = {Id} Title = {Title}";
        }
    }

    public interface IDrawback
    {
        int                Id    { get; set; }
        ICollection<Club>? Clubs { get; set; }
    }

    /// <summary>
    ///     Properties are included in Drawback table.
    /// </summary>
    [ComplexType]
    public class DrawbackDetails
    {
        public DateTime? DateCreated { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"DrawbackDetail : Description = {Description}";
        }
    }
}