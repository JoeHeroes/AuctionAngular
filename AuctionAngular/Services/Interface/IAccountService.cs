using AuctionAngular.DTO;

namespace AuctionAngular.Services.Interface
{
    public interface IAccountService
    {
        Task<string> GeneratJwt(LoginDto dto);
        Task RegisterUser(RegisterUserDto dto);
        Task RestartPassword(RestartPasswordDto dto);
    }
}
