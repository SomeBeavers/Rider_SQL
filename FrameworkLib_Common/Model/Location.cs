using System;
using System.Data.Entity.Spatial;
using System.Linq;

namespace FrameworkLib_Common.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;

        public virtual Club        Club               { get; set; } = null!;
        public virtual DbGeography GeographicLocation { get; set; } = null!;

        public override string ToString()
        {
            return $@"Location: Address = {Address} GeographicLocation = {GeographicLocation}";
        }
    }
}