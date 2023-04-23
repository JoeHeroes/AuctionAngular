using AuctionAngular.DTO;

namespace AuctionAngular.Services.Interface
{
    public interface IAuctionService
    {
        Task<bool> LiveAuction();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionList();
        Task EndAuction();
    }
}
