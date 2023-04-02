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
    }
}
