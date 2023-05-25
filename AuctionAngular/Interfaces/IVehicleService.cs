using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface IVehicleService
    {
        Task<int> Create(CreateVehicleDto dto);
        Task Delete(int id);
        Task<IEnumerable<ViewVehiclesDto>> GetAll();
        Task<ViewVehicleDto> GetById(int id);
        Task Update(int id, EditVehicleDto dto);
        Task Bid(UpdateBidDto dto);
        Task Watch(WatchDto dto);
        Task RemoveWatch(WatchDto dto);
        Task<bool> CheckWatch(WatchDto dto);
        Task<IEnumerable<ViewVehiclesDto>> GetAllWatch(int id);
        Task<List<string>> AddPicture(int id, IFormFileCollection files);
    }
}
