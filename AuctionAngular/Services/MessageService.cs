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

        public bool Check(string token, string email)
        {

            var result =  _dbContext.Messages.Where(x => x.Email == email).OrderByDescending(s => s.Date).Where(t => t.Data == token);
        
            if(result != null)
            {
                return true;
            }

            return false;
        }

        public async Task Create(Message dto)
        {
            _dbContext.Messages.Add(dto);
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
