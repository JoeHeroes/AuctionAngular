using AuctionAngular.DTO;
using AuctionAngular.Models;
using AuctionAngular.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuctionAngular.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuctionDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSetting;
        public AccountService(AuctionDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSetting)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSetting = authenticationSetting;
        }
        public async Task RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {

                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = dto.Password,
                RoleId = dto.RoleId,
                ProfilePicture = ""

            };

            var hashedPass = this.passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPass;
            this.dbContext.Users.Add(newUser);
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<string> GeneratJwt(LoginDto dto)
        {
            var user = await this.dbContext
                 .Users
                 .FirstOrDefaultAsync(u => u.Email == dto.Email);


            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }


            var result = this.passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var clasims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                
            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                clasims.Add(
                    new Claim("Nationality", user.Nationality)
                    );
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSetting.JwtExpireDays);


            var token = new JwtSecurityToken(authenticationSetting.JwtIssuer,
                authenticationSetting.JwtIssuer,
                clasims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHander = new JwtSecurityTokenHandler();
            return tokenHander.WriteToken(token);
        }

        public async Task RestartPassword(RestartPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                throw new BadRequestException("New Password must be the same");
            }


            if (dto.OldPassword == dto.NewPassword)
            {
                throw new BadRequestException("New and Old Password and couldn't be the same");
            }


            var account = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);


            var result = this.passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.OldPassword);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Old password is invalid");
            }

            account.PasswordHash = this.passwordHasher.HashPassword(account, dto.NewPassword); ;
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }
    }
}