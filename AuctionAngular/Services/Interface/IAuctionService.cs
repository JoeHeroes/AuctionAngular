using AuctionAngular.DTO;

namespace AuctionAngular.Services.Interface
{
    public interface IAuctionService
    {
        Task<bool> LiveAuction();
        Task EndAuction();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionList();
        Task<IEnumerable<ViewVehicleDto>> AuctionList();
    }
}
