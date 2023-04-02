using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.DTO
{
    public class UpdateBidDto
    {
        public int lotNumber { get; set; }
        public int bidNow { get; set; }

    }
}
