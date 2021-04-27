namespace CoreLib_Common.Model
{
    //[Table("Beaver")]
    public class Beaver : Animal
    {
        public FluffinessEnum Fluffiness { get; set; }
        public int Size { get; set; }
        public override string ToString()
        {
            return @$"{base.ToString()} Beaver: Fluffiness = {this.Fluffiness} Size = {this.Size}";
        }
    }

    public enum FluffinessEnum
    {
        NotFluffy,
        Fluffy,
        VeryFluffy
    }
}