using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface IAccountService
    {
        Task<string> GeneratJwt(LoginDto dto);
        Task RegisterUser(RegisterUserDto dto);
        Task RestartPassword(RestartPasswordDto dto);
        Task<ViewUserDto> GetUserInfo(int id);
        Task EditProfile(EditUserDto dto);
        Task<IEnumerable<RoleDto>> GetRole();
        Task<string> AddPicture(int id, IFormFile file);
    }
}
