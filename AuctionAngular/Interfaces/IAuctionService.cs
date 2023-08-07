using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface IAuctionService
    {
        Task<bool> LiveAuction();
        Task EndAuction();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionList();
        Task<IEnumerable<ViewVehicleDto>> AuctionList();
        IEnumerable<ViewVehicleDto> AuctionListSpecial();
    }
}
