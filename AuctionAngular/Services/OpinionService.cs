using AuctionAngular.Dtos.Opinion;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public class OpinionService : IOpinionService
    {
        private readonly AuctionDbContext _dbContext;
        public OpinionService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateOpinionAsync(CreateOpinionDto dto)
        {
            var opinionExist = await _dbContext.Opinions.FirstOrDefaultAsync(x => x.VehicleId == dto.VehicleId);
            if (opinionExist != null)
                throw new Exception("Opinion exist");

            var opinion = new Opinion
            {
                Description = dto.Description,
                Origin = dto.Origin,
                Valuation = dto.Valuation,
                Condition = dto.Condition,
                DescriptionConditionInside = dto.DescriptionConditionInside,
                DescriptionConditionBodywork = dto.DescriptionConditionBodywork,
                ComplianceWithVIN = dto.ComplianceWithVIN,
                VehicleId = dto.VehicleId,
            };

            await _dbContext.Opinions.AddAsync(opinion);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return opinion.Id;
        }

        public async Task<ViewOpinionDto> GetByVehicleIdOpinionAsync(int id)
        {
            var opinion = await _dbContext
                .Opinions
                .FirstOrDefaultAsync(u => u.VehicleId == id);

            if (opinion is null)
                throw new NotFoundException("Opinion not found.");

            return ViewOpinionDtoConvert(opinion);
        }

        public ViewOpinionDto ViewOpinionDtoConvert(Opinion opinion)
        {
            return new ViewOpinionDto()
            {
                Description = opinion.Description,
                Origin = opinion.Origin,
                Valuation = opinion.Valuation,
                Condition = opinion.Condition,
                DescriptionConditionInside = opinion.DescriptionConditionInside,
                DescriptionConditionBodywork = opinion.DescriptionConditionBodywork,
                ComplianceWithVIN = opinion.ComplianceWithVIN,
            };
        }
    }
}

