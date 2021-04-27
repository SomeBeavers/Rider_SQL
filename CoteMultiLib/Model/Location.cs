using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoreMultiLib.Model
{
    [Owned]
    public class Location
    {
        public string Address { get; set; } = null!;

        public virtual Club Club { get; set; } = null!;

        public override string ToString()
        {
            return $@"Location: Address = {Address}";
        }
    }
}