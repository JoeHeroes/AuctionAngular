using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Dtos.User
{
    public class RestartPasswordDto
    {

        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DisplayName("Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

    }
}
