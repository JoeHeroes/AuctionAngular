using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.DTO
{
    public class UserAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
