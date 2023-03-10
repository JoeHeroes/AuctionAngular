namespace AuctionAngular.Models
{
    public class Watch
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public User UserMany { get; set; }
        public Vehicle VehicleMany { get; set; }
    }
}
