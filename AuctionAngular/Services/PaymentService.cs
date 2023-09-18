using AuctionAngular.Dtos.Payment;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AuctionDbContext _dbContext;
        public PaymentService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreatePaymentAsync(CreatePaymentDto dto)
        {

            var result = await _dbContext
              .Payments
              .FirstOrDefaultAsync(x => x.LotId == dto.LotId);

            if(result != null)
            {
                return result.Id;
            }


            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(u => u.Id == dto.LotId);

            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(u => u.Id == dto.AuctionId);

            var payment = new Payment
            {
                SaleDate = auction!.DateTime,
                LotId = dto.LotId,
                LocationId = auction.LocationId,
                Description = dto.Description,
                InvoiceAmount = dto.InvoiceAmount,
                LastInvoicePaidDate = DateTime.Now,
                LotLeftLocationDate = dto.LotLeftLocationDate,
                StatusSell = false,
                InvoiceGenereted = false,
                UserId = vehicle!.WinnerId
            };


            await _dbContext.Payments.AddAsync(payment);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return payment.Id;
        }

        public async Task DeletePaymentAsync(int id)
        {
            var result = _dbContext
               .Payments
               .FirstOrDefault(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Payment not found.");
            }

            _dbContext.Payments.Remove(result);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }
        public async Task<Payment> UpdatePaymentAsync(int id, EditPaymentDto dto)
        {
            var result = await _dbContext
               .Payments
               .FirstOrDefaultAsync(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            result.InvoiceAmount = dto.InvoiceAmount;
            result.LotLeftLocationDate = dto.LotLeftLocationDate;
            result.StatusSell = dto.StatusSell;
            result.InvoiceGenereted = dto.InvoiceGenereted;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return result;
        }

        public async Task<ViewPaymentDto> GetByIdPaymentAsync(int id)
        {
            var payment = await _dbContext
               .Payments
               .FirstOrDefaultAsync(u => u.Id == id);

            if (payment is null)
            {
                throw new NotFoundException("Payment not found.");
            }

            return ViewPaymentDtoConvert(payment);
        }

        public async Task<IEnumerable<ViewPaymentDto>> GetPaymentsAsync(int userId)
        {
            var payments = await _dbContext
                .Payments
                .ToListAsync();

            List<ViewPaymentDto> viewPayment = new List<ViewPaymentDto>();

            foreach (var payment in payments)
            {
                if(payment.UserId == userId)
                {
                    viewPayment.Add(ViewPaymentDtoConvert(payment));
                }
            }
            return viewPayment;
        }

        public ViewPaymentDto ViewPaymentDtoConvert(Payment payment)
        {
            var location = _dbContext.Locations.FirstOrDefault(x => x.Id == payment.LocationId);

            return new ViewPaymentDto()
            {
                SaleDate = payment.SaleDate,
                LotId = payment.LotId,
                Location = location!.Name,
                Description = payment.Description,
                InvoiceAmount = payment.InvoiceAmount,
                LastInvoicePaidDate = payment.LastInvoicePaidDate,
                LotLeftLocationDate = payment.LotLeftLocationDate,
                StatusSell = payment.StatusSell,
                InvoiceGenereted = payment.InvoiceGenereted
            };
        } 
    }
}
