using AuctionAngular.Dtos.Role;
using AuctionAngular.Dtos.User;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
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
        private readonly AuctionDbContext _dbContext;
        private readonly AuthenticationSettings _authenticationSetting;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IWebHostEnvironment _webHost;
        /// <inheritdoc/>
        public AccountService(AuctionDbContext dbContext,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSetting,
            IWebHostEnvironment webHost)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSetting = authenticationSetting;
            _webHost = webHost;
        }
        public async Task<User> CreateUserAsync(RegisterUserDto dto)
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
                isConfirmed = false
            };

            var user =  await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user != null)
            {
                throw new Exception("Email is taken.");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception("Password and ConfirmPassword must be the same.");
            }

            if (dto.DateOfBirth > DateTime.Now.Date.AddDays(1))
            {
                throw new Exception("Are you time traveler?");
            }

            var hashedPass = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPass;
            await _dbContext.Users.AddAsync(newUser);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return newUser;
        }

        public async Task<string> LoginUserAsync(LoginUserDto dto)
        {
            var account = await _dbContext
                 .Users
                 .FirstOrDefaultAsync(u => u.Email == dto.Email);


            if (account is null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            if (account.isConfirmed == false)
            {
                throw new BadRequestException("Confirm your email.");
            }

            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            return await GenerateTokenAsync(account);
        }

        public async Task<bool> RestartPasswordAsync(RestartPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                throw new BadRequestException("New Password must be the same.");
            }

            if (dto.OldPassword == dto.NewPassword)
            {
                throw new BadRequestException("New and Old Password and couldn't be the same.");
            }

            var account = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.OldPassword);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Old password is invalid.");
            }

            account.PasswordHash = _passwordHasher.HashPassword(account, dto.NewPassword); ;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return true;
        }

        public async Task<ViewUserDto> GetUserInfoAsync(int id)
        {
            var user = await _dbContext
               .Users
               .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
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

        public async Task<ViewRoleDto> GetUserRoleAsync(int id)
        {
            var user = await _dbContext
               .Users
               .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }


            var role = await _dbContext
               .Roles
               .FirstOrDefaultAsync(u => u.Id == user.RoleId);

            if (role is null)
            {
                throw new NotFoundException("Role not found.");
            }

            return new ViewRoleDto() { Name = role.Name };
        }


        public async Task EditProfileAsync(EditUserDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            user.Name = dto.Name!="" ? dto.Name: user.Name;
            user.SureName = dto.SureName != "" ? dto.SureName : user.SureName;
            user.Phone = dto.Phone != "" ? dto.Phone : user.Phone;
            user.Nationality = dto.Nationality != "" ? dto.Nationality : user.Nationality;
            user.DateOfBirth = dto.DateOfBirth.ToString() != default(DateTime).ToString() ? dto.DateOfBirth : user.DateOfBirth;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            var roles = await _dbContext
                .Roles
                .ToListAsync();

            if (roles is null)
            {
                throw new NotFoundException("Roles not found.");
            }

            List<Role> result = new List<Role>();

            foreach (var role in roles)
            {
                var roleDto = new Role { Id = role.Id, Name = role.Name };
                result.Add(roleDto);
            }

            return result;
        }
        
        public async Task<string> AddProfilePictureAsync(int id, IFormFile file)
        {
            string fileName = "";
            if (file != null)
            {
                string uploadDir = Path.Combine(_webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                user.ProfilePicture = fileName;
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return fileName;
        }

        public async Task<string> GenerateTokenAsync(User user)
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSetting.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSetting.JwtIssuer,
                _authenticationSetting.JwtIssuer,
                clasims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHander = new JwtSecurityTokenHandler();
            return tokenHander.WriteToken(token);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AccountVerification(int id ,bool autorization)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.isConfirmed = autorization;

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