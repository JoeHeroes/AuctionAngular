using System;
using System.ComponentModel;

namespace AuctionAngular.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
        [DisplayName("Role")]
        public int RoleId { get; set; }
        public string ProfilePicture { get; set; }
    }
}
