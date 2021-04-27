using System;
using System.Linq;

namespace CoreMultiLib.Model
{
    // Table-valued functions
    public class AnimalLocation
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public override string ToString()
        {
            return $"AnimalLocation : Name = {Name} Address = {Address}";
        }
    }
}