using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IAuctionService
    {
        Task<bool> LiveAuction();
        Task StartAuction();
        Task EndAuction();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionList();
        Task<IEnumerable<Auction>> AuctionList();
    }
}
