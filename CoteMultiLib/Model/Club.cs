using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CoreMultiLib.Model
{
    public interface IClub
    {
        int                    Id            { get; set; }
        string                 Title         { get; set; }
        NotMappedText          LocalizedText { get; set; }
        ICollection<Animal>?   Animals       { get; set; }
        ICollection<Location>  Locations     { get; set; }
        ICollection<Grade>     Grades        { get; set; }
        ICollection<Drawback>? Drawbacks     { get; set; }
    }

    public class Club : IClub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public NotMappedText LocalizedText { get; set; } = null!;

        public virtual ICollection<Animal>? Animals { get; set; }
        public virtual ICollection<Location> Locations { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; } = null!;
        public virtual ICollection<Drawback>? Drawbacks { get; set; }

        public override string ToString()
        {
            return $@"Club: Id = {Id} Title = {Title}";
        }
    }

    [NotMapped]
    public class NotMappedText
    {
        public string LocalizedText { get; set; } = null!;
    }
}