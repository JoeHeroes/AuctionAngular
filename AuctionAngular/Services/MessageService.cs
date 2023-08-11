using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public class MessageService : IMessageService
    {
        private readonly AuctionDbContext _dbContext;
        public MessageService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckMessageAsync(string token, string email)
        {
            var result =  await _dbContext.Messages.Where(x => x.Email == email).OrderByDescending(s => s.Date).Where(t => t.Data == token).FirstOrDefaultAsync();
        
            if(result != null)
            {
                return true;
            }

            return false;
        }

        public async Task CreateMessageAsync(Message dto)
        {
            await _dbContext.Messages.AddAsync(dto);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

        }
    }
}
