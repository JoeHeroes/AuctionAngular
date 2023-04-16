using AuctionAngular.DTO;
using AuctionAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Services.Interface
{
    public interface IAccountService
    {
        Task<string> GeneratJwt(LoginDto dto);
        Task RegisterUser(RegisterUserDto dto);
        Task RestartPassword(RestartPasswordDto dto);
        Task<ViewUserDto> GetUserInfo(int id);
        Task EditProfile(EditUserDto dto);

    }
}
