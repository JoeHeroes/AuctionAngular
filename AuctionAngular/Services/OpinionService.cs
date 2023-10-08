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
            var opinion = new Opinion
            {
                Description = dto.Description,
                Origin = dto.Origin,
                Valuation = dto.Valuation,
                assessConditionInside = dto.assessConditionInside,
                assessConditionOutsite = dto.assessConditionOutsite,
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

        public async Task<ViewOpinionDto> GetByIdOpinionAsync(int id)
        {
            var opinion = await _dbContext
                .Opinions
                .FirstOrDefaultAsync(u => u.Id == id);

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
                assessConditionInside = opinion.assessConditionInside,
                assessConditionOutsite = opinion.assessConditionOutsite,
            };
        }
    }
}

