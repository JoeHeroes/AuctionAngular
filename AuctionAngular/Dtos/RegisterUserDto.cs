using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } 
        [Required]
        public string Nationality { get; set; }
        [Required]
        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [DisplayName("Role")]
        public int RoleId { get; set; } = 1;
        //1 Buyer 
        //2 Seller
        //3 Admin
    }
}
