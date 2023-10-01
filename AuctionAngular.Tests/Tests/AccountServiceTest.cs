using AuctionAngular.Dtos.User;
using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Database.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace AuctionAngular.Tests
{
    public class AccountServiceTest
    {
        private AuctionDbContext _dbContext;
        private Mock<IPasswordHasher<User>> _passwordHasher;
        private Mock<AuthenticationSettings> _authenticationSetting;
        private Mock<IWebHostEnvironment> _webHost;

        private AccountService accountService;

        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDBTest; Trusted_Connection=True";

        static AccountServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public AccountServiceTest()
        {
            _dbContext = new AuctionDbContext(dbContextOptions);


            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(_dbContext);

            _passwordHasher = new Mock<IPasswordHasher<User>>();
            _passwordHasher.Setup(m => m.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                            .Returns("Hash value");
            _authenticationSetting = new Mock<AuthenticationSettings>();
            _webHost = new Mock<IWebHostEnvironment>();

            accountService = new AccountService(_dbContext, _passwordHasher.Object, _authenticationSetting.Object, _webHost.Object);
        }

        [Fact]
        public async Task CreateUserAsync_WithValidData_ShouldCreateUser()
        {
            //Arrange

            var dto = new RegisterUserDto()
            {
                Email = "Test1@wpl",
                Password = "Password12#",
                ConfirmPassword = "Password12#",
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now,
                RoleId = 1,
            };

            //Act

            var user = await accountService.CreateUserAsync(dto);

            //Assert

            Assert.NotNull(user);
            Assert.Equal(dto.Email, user.Email);
            Assert.Equal(dto.Name, user.Name);
            Assert.Equal(dto.SureName, user.SureName);
            Assert.Equal(dto.DateOfBirth, user.DateOfBirth);
            Assert.Equal(dto.Nationality, user.Nationality);
            Assert.Equal(dto.RoleId, user.RoleId);
        }

        [Fact]
        public async Task CreateUserAsync_WithTakenEmial_ShouldNotCreateUser()
        {
            //Arrange

            var dto = new RegisterUserDto()
            {
                Email = "Test2@wpl",
                Password = "Password12#",
                ConfirmPassword = "Password12#",
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now,
                RoleId = 1,
            };

            //Act

            Func<Task> act = () => accountService.CreateUserAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);

            Assert.Equal("Email is taken.", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WithPasswordNotSame_ShouldNotCreateUser()
        {
            //Arrange

            var dto = new RegisterUserDto()
            {
                Email = "Test3@wpl",
                Password = "Password123",
                ConfirmPassword = "Password12#",
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now,
                RoleId = 1,
            };

            //Act

            Func<Task> act = () => accountService.CreateUserAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);

            Assert.Equal("Password and ConfirmPassword must be the same.", exception.Message);
        }


        [Fact]
        public async Task CreateUserAsync_WithCantBornInFuture_ShouldNotCreateUser()
        {
            //Arrange

            var dto = new RegisterUserDto()
            {
                Email = "Test4@wpl",
                Password = "Password12#",
                ConfirmPassword = "Password12#",
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now.AddDays(1),
                RoleId = 1,
            };

            //Act

            Func<Task> act = () => accountService.CreateUserAsync(dto);


            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);

            Assert.Equal("Are you time traveler?", exception.Message);
        }

        [Fact]
        public async Task LoginUserAsync_WithValidData_ShouldNotLogin()
        {
            //Arrange

            var dto = new LoginUserDto()
            {
                Email = "Test5@wpl",
                Password = "Password12#",

            };

            //Act

            var token = accountService.LoginUserAsync(dto);

            //Assert

            Assert.NotNull(token);

        }

        [Fact]
        public async Task LoginUserAsync_WithEmailNotFound_ShouldNotLogin()
        {
            //Arrange

            var dto = new LoginUserDto()
            {
                Email = "Test6@wpl",
                Password = "Password12#",

            };

            //Act

            Func<Task> act = () => accountService.LoginUserAsync(dto); ;

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Invalid username or password.", exception.Message);
        }

        [Fact]
        public async Task LoginUserAsync_WithAccountNotConfirm_ShouldNotLogin()
        {
            //Arrange

            var dto = new LoginUserDto()
            {
                Email = "Test7@wpl",
                Password = "Password12#",

            };


            //Act

            Func<Task> act = () => accountService.LoginUserAsync(dto); ;

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Confirm your email.", exception.Message);
        }

        [Fact]
        public async Task RestartPasswordAsync_WithOldPasswordInvalid_ShouldNotChangePassword()
        {
            //Arrange

            var dto = new RestartPasswordDto()
            {
                Email = "Test9@wpl",
                OldPassword = "Password12#",
                NewPassword = "Password13#",
                ConfirmNewPassword = "Password13#",
            };

            //Act

            Func<Task> act = () => accountService.RestartPasswordAsync(dto); ;

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Old password is invalid.", exception.Message);
        }

        [Fact]
        public async Task RestartPasswordAsync_WithNewPassowrdNotSame_ShouldNotChangePassword()
        {
            //Arrange

            var dto = new RestartPasswordDto()
            {
                Email = "Test9@wpl",
                OldPassword = "Password12#",
                NewPassword = "Password13#",
                ConfirmNewPassword = "Password14#",
            };

            //Act

            Func<Task> act = () => accountService.RestartPasswordAsync(dto); ;

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("New Password must be the same.", exception.Message);
        }


        [Fact]
        public async Task RestartPasswordAsync_WithNewPassowrdAndOldSame_ShouldChangePassword()
        {
            //Arrange

            var dto = new RestartPasswordDto()
            {
                Email = "Test10@wpl",
                OldPassword = "Password12#",
                NewPassword = "Password12#",
                ConfirmNewPassword = "Password12#",
            };

            //Act

            Func<Task> act = () => accountService.RestartPasswordAsync(dto); ;

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("New and Old Password and couldn't be the same.", exception.Message);
        }



        [Fact]
        public async Task GetUserInfoByIdAsync_WithCorrectId_RetrunUser()
        {
            //Arrange

            var dto = new User()
            {
                Email = "Test10@wpl",
                PasswordHash = "Password12#",
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now.Date,
                RoleId = 1,
                ProfilePicture = "",
                EmialConfirmed = true
            };

            //Act

            var user = await accountService.GetUserInfoAsync(5);

            //Assert

            Assert.NotNull(user);
            Assert.Equal(dto.Email, user.Email);
            Assert.Equal(dto.Name, user.Name);
            Assert.Equal(dto.SureName, user.SureName);
            Assert.Equal(dto.DateOfBirth, user.DateOfBirth);
            Assert.Equal(dto.Nationality, user.Nationality);
            Assert.Equal(dto.Phone, user.Phone);
            Assert.Equal(dto.ProfilePicture, user.ProfilePicture);
        }

        [Fact]
        public async Task GetUserInfoByIdAsync_WithBadId_ReturnNotFoundException()
        {
            //Arrange

            //Act

            Func<Task> act = () => accountService.GetUserInfoAsync(999); ;

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("User not found.", exception.Message);
        }



        [Fact]
        public async Task EditProfileAsync_WithValidData_EditUser()
        {
            //Arrange

            var dto = new EditUserDto()
            {
                UserId = 5,
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now.Date,
            };

            //Act
            await accountService.EditProfileAsync(dto);

            var user = await accountService.GetUserInfoAsync(dto.UserId);

            //Assert

            Assert.NotNull(user);
            Assert.Equal(dto.Name, user.Name);
            Assert.Equal(dto.SureName, user.SureName);
            Assert.Equal(dto.DateOfBirth, user.DateOfBirth);
            Assert.Equal(dto.Nationality, user.Nationality);
            Assert.Equal(dto.Phone, user.Phone);
        }

        [Fact]
        public async Task EditProfileAsync_WithBadUserId_ReturnNotFoundException()
        {
            //Arrange

            var dto = new EditUserDto()
            {
                UserId = 999,
                Name = "Test",
                SureName = "Test",
                Nationality = "Poland",
                Phone = "+48 123 456 789",
                DateOfBirth = DateTime.Now.Date,
            };

            //Act

            Func<Task> act = () => accountService.EditProfileAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("User not found.", exception.Message);
        }

        [Fact]
        public async Task GetAllRoleAsync_ReturnListRoles()
        {
            //Arrange

            //Act

            var roles = await accountService.GetAllRoleAsync();

            Assert.NotNull(roles);
        }

        [Fact]
        public async Task GetByEmailAsync_ReturnUser()
        {
            //Arrange

            //Act

            var user = await accountService.GetByEmailAsync("Test10@wpl");

            Assert.NotNull(user);
            Assert.Equal("Test10@wpl", user.Email);
        }
    }
}
