
namespace Database.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool isSent { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; } = 0;
    }
}
