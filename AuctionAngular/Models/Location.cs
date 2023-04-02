using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Picture { get; set; }
    }
}
