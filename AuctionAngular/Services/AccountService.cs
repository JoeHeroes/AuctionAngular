using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IWebHostEnvironment webHost;
        private readonly IConfiguration configuration;
        /// <inheritdoc/>
        public AccountService(AuctionDbContext dbContext,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSetting,
            IWebHostEnvironment webHost,
            IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSetting = authenticationSetting;
            this.webHost = webHost;
            this.configuration = configuration;
        }
        public async Task CreateUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                SureName = dto.SureName,
                PasswordHash = dto.Password,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                Phone = dto.Phone,
                RoleId = dto.RoleId,
                ProfilePicture = "",
                EmialConfirmed = false
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

        public async Task<string> LoginUser(LoginUserDto dto)
        {
            var user = await this.dbContext
                 .Users
                 .FirstOrDefaultAsync(u => u.Email == dto.Email);


            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            return await GenerateToken(user);
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

        public async Task<ViewUserDto> GetUserInfo(int id)
        {
            var user = await this.dbContext
               .Users
               .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var result = new ViewUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                SureName = user.SureName,
                DateOfBirth = user.DateOfBirth,
                Nationality = user.Nationality,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePicture
            };

            return result;
        }

        public async Task EditProfile(EditUserDto dto)
        {
            var user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            user.Name = dto.Name;
            user.SureName = dto.SureName;
            user.Phone = dto.Phone;
            user.Nationality = dto.Nationality;
            user.DateOfBirth = dto.Date;

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<IEnumerable<RoleDto>> GetRole()
        {
            var roles = await this.dbContext
                .Roles
                .ToListAsync();

            List<RoleDto> result = new List<RoleDto>();

            foreach (var role in roles)
            {
                var roleDto = new RoleDto() { Id = role.Id, Name = role.Name };
                result.Add(roleDto);
            }

            return result;
        }
        
        public async Task<string> AddPicture(int id, IFormFile file)
        {
            string fileName = "";
            if (file != null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                user.ProfilePicture = fileName;
            }

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return fileName;
        }

        public async Task<string> GenerateToken(User user)
        {
            var clasims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.Name} {user.SureName}"),
                new Claim("UserId", user.Id.ToString()),
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
    }
}