using System.ComponentModel;

namespace AuctionAngular.Dtos.User
{
    public class ViewUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }

        public string ProfilePicture { get; set; }
    }
}
