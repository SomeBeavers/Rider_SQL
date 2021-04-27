using System;
using System.Linq;

namespace CoreMultiLib.Model
{
    public class AnimalClub
    {
        public int AnimalId { get; set; }
        public int ClubId { get; set; }

        public virtual Animal Animal { get; set; } = null!;
        public virtual Club Club { get; set; } = null!;

        public DateTime PublicationDate { get; set; }
    }
}