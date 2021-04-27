namespace CoreLib_Common.Model
{
    public class MapToQuery
    {
        public int Id { get; set; }
        public FluffinessEnum Fluffiness { get; set; }
        public int Size { get; set; }
        public virtual Club Club { get; set; } = null!;
        public virtual int ClubId { get; set; }

        public override string ToString()
        {
            return @$"MapToQuery: Id = {Id}";
        }
    }
}