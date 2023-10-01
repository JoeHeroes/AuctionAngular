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
        public string Url { get; set; }
        public bool isAllDay { get; set; }
        public int UserId { get; set; } = 0;
    }
}
