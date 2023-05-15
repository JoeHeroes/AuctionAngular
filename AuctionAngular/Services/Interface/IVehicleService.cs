using AuctionAngular.DTO;
using AuctionAngular.Models.DTO;

namespace AuctionAngular.Services.Interface
{
    public interface IVehicleService
    {
        Task<int> Create(CreateVehicleDto dto);
        Task Delete(int id);
        Task<IEnumerable<ViewVehiclesDto>> GetAll();
        Task<ViewVehicleDto> GetById(int id);
        Task Update(EditVehicleDto dto);
        Task Bid(UpdateBidDto dto);
        Task Watch(WatchDto dto);
        Task RemoveWatch(WatchDto dto);
        Task<bool> CheckWatch(WatchDto dto);
        Task<IEnumerable<ViewVehiclesDto>> GetAllWatch(int id);
        Task<List<string>> AddPicture(int id, IFormFileCollection files);
    }
}
