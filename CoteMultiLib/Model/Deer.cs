using System;
using System.Linq;

namespace CoreMultiLib.Model
{
    //[Table("Deer")]
    public class Deer : Animal
    {
        public bool Horns { get; set; }

        public override string ToString()
        {
            return @$"{base.ToString()} Deer : Horns = {this.Horns}";
        }
    }
}