using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkLib_Common.Model
{
    //[Table("Crow")]
    public class Crow : Animal
    {
        public string Color { get; set; } = null!;

        [NotMapped]
        public int Size { get; set; }

        public override string ToString()
        {
            return @$"{base.ToString()} Crow : Color = {this.Color} Size = {this.Size} (cause [NotMapped])";
        }
    }
}