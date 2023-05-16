using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Dtos
{
    public class ViewLocationDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Picture { get; set; }
    }
}
