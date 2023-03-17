using AuctionAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{

    public interface IUserService
    {
        IQueryable<User> GetUsers(Guid id);
        void CreateAsync(User userId);
        void RemoveAsync(int id);
        Task<User> FindByEmailAsync(string email);
    }
    public class UserService : IUserService
    {



        private readonly AuctionDbContext dbContext;
        public UserService(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void CreateAsync(User userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return  this.dbContext
                     .Users
                     .Include(e => e.Email)
                     .FirstOrDefaultAsync(u => u.Email == email);
        }

        public IQueryable<User> GetUsers(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}


