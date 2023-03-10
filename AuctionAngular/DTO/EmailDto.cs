using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.DTO
{
    public class EmailDto
    {
        [EmailAddress]
        public string Email{ get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        [MaxLength(200)]
        public string Body { get; set; }

    }
}
