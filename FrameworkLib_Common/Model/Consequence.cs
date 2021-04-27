namespace FrameworkLib_Common.Model
{
    public class Consequence
    {
        public string Name { get; set; } = null!;

        public override string ToString()
        {
            return $"Consequence : Name = {Name}";
        }
    }
}