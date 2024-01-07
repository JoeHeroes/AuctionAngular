using AuctionAngular.Dtos.Auction;
using AuctionAngular.Dtos.Vehicle;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IAuctionService
    {
        Task<bool> LiveAuctionAsync();
        Task<bool> StartedAuctionAsync();
        Task StartAuctionAsync();
        Task EndAuctionAsync();
        Task CreateAuctionAsync(CreateAuctionDto dto);
        Task DeleteAuctionAsync(int id);
        Task EditAuctionAsync(EditAuctionDto dto);
        Task<ViewAuctionDto> GetByIdAuctionAsync(int id);
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionListAsync();
        Task<IEnumerable<ViewAuctionDto>> AuctionListAsync();
        Task<ViewVehicleDto> ViewVehicleDtoConvert(Vehicle vehicle, List<string> pictures);
        Task<ViewAuctionDto> ViewAuctionDtoConvert(Auction auction);
    }
}
