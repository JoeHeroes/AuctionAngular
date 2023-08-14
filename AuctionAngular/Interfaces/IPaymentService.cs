using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePaymentAsync(CreatePaymentDto dto);
        Task DeletePaymentAsync(int id);
        Task<IEnumerable<ViewPaymentDto>> GetPaymentsAsync();
        Task<ViewPaymentDto> GetByIdPaymentAsync(int id);
        Task<Payment> UpdatePaymentAsync(int id, EditPaymentDto dto);
    }
}
