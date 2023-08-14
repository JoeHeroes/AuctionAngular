using AuctionAngular.Dtos;
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
            var payment = new Payment
            {
                SaleDate = dto.SaleDate,
                LotId = dto.LotId,
                LocationId = dto.LocationId,
                Description = dto.Description,
                InvoiceAmount = dto.InvoiceAmount,
                LastInvoicePaidDate = DateTime.Now,
                LotLeftLocationDate = dto.LotLeftLocationDate,
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

            result.LocationId = dto.LocationId;
            result.Description = dto.Description;
            result.InvoiceAmount = dto.InvoiceAmount;
            result.LotLeftLocationDate = dto.LotLeftLocationDate;

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

        public async Task<IEnumerable<ViewPaymentDto>> GetPaymentsAsync()
        {
            var payments = await _dbContext
                .Payments
                .ToListAsync();

            List<ViewPaymentDto> viewPayment = new List<ViewPaymentDto>();

            foreach (var payment in payments)
            {
                viewPayment.Add(ViewPaymentDtoConvert(payment));
            }

            return viewPayment;
        }


        public ViewPaymentDto ViewPaymentDtoConvert(Payment payment)
        {
            return new ViewPaymentDto()
            {
                SaleDate = payment.SaleDate,
                LotId = payment.LotId,
                LocationId = payment.LocationId,
                Description = payment.Description,
                InvoiceAmount = payment.InvoiceAmount,
                LastInvoicePaidDate = payment.LastInvoicePaidDate,
                LotLeftLocationDate = payment.LotLeftLocationDate,
            };
        } 
    }
}
