
using AuctionAngular.Dtos.Opinion;

namespace AuctionAngular.Interfaces
{
    public interface IOpinionService
    {
        Task<int> CreateOpinionAsync(CreateOpinionDto dto);
        Task<ViewOpinionDto> GetByIdOpinionAsync(int id);
    }
}
