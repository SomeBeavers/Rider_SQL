using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FrameworkLib_Common.Model
{
    public class Club
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public NotMappedText LocalizedText { get; set; } = null!;

        public virtual ICollection<Animal>?   Animals   { get; set; }
        public virtual ICollection<Location>? Locations { get; set; }
        public virtual ICollection<Grade>     Grades    { get; set; } = null!;
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