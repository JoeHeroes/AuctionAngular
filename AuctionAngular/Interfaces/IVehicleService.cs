using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface IVehicleService
    {
        Task<int> CreateVehicleAsync(CreateVehicleDto dto);
        Task DeleteVehicleAsync(int id);
        Task<IEnumerable<ViewVehiclesDto>> GetVehiclesAsync();
        Task<IEnumerable<ViewVehiclesDto>> GetAllBidedAsync(int id);
        Task<IEnumerable<ViewVehiclesDto>> GetAllWonAsync(int id);
        Task<IEnumerable<ViewVehiclesDto>> GetAllLostAsync(int id);
        Task<ViewVehicleDto> GetByIdVehicleAsync(int id);
        Task UpdateVehicleAsync(int id, EditVehicleDto dto);
        Task<bool> BidVehicleAsync(UpdateBidDto dto);
        Task WatchVehicleAsync(WatchDto dto);
        Task RemoveWatchAsync(WatchDto dto);
        Task<bool> CheckWatchAsync(WatchDto dto);
        Task<IEnumerable<ViewVehiclesDto>> GetAllWatchAsync(int id);
        Task<List<string>> AddPictureAsync(int id, IFormFileCollection files);
    }
}
