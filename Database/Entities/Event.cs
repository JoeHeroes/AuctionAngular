namespace Database.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        public bool AllDay { get; set; }
        public int Owner { get; set; } = 0;
    }
}
