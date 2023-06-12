using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginUser(LoginUserDto dto);
        Task CreateUser(RegisterUserDto dto);
        Task RestartPassword(RestartPasswordDto dto);
        Task<ViewUserDto> GetUserInfo(int id);
        Task EditProfile(EditUserDto dto);
        Task<IEnumerable<RoleDto>> GetRole();
        Task<string> AddPicture(int id, IFormFile file);
        Task<string> GenerateToken(User user);
    }
}
