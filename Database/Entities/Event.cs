namespace Database.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Color { get; set; }
        public bool AllDay { get; set; }
        public int Owner { get; set; } = 0;
    }
}
