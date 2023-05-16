using System.ComponentModel;

namespace AuctionAngular.Dtos
{
    public class ViewUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ProfilePicture { get; set; }
    }
}
