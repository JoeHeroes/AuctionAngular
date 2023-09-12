using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginUserAsync(LoginUserDto dto);
        Task<User> CreateUserAsync(RegisterUserDto dto);
        Task<bool> RestartPasswordAsync(RestartPasswordDto dto);
        Task<ViewUserDto> GetUserInfoAsync(int id);
        Task<ViewRoleDto> GetUserRoleAsync(int id);
        Task EditProfileAsync(EditUserDto dto);
        Task<IEnumerable<RoleDto>> GetAllRoleAsync();
        Task<string> AddProfilePictureAsync(int id, IFormFile file);
        Task<string> GenerateTokenAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task AccountVerification(int id, bool autorization);
    }
}
