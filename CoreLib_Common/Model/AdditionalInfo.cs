using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CoreLib_Common.Model
{
    public class AdditionalInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Comment { get; set; }

        public virtual ICollection<Club>? Clubs { get; set; }

        public override string ToString()
        {
            return $@"AdditionalInfo: Id = {Id} Comment = {Comment}";
        }
    }

    public class AdditionalInfoClub
    {
        public int AdditionalInfoId { get; set; }
        public int ClubId           { get; set; }

        public virtual AdditionalInfo AdditionalInfo { get; set; } = null!;
        public virtual Club           Club           { get; set; } = null!;
    }
}