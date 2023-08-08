using AuctionAngular;
using AuctionAngular.Dtos;
using AuctionAngular.Services;
using Database;
using Database.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace AuctionAngularTests
{
    public class AccountServiceTest
    {

        private AuctionDbContext _dbContext;
        private Mock<IPasswordHasher<User>> _passwordHasher;
        private Mock<AuthenticationSettings> _authenticationSetting;
        private Mock<IWebHostEnvironment> _webHost;


        private AccountService accountService;


        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDB; Trusted_Connection=True";

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
            _authenticationSetting = new Mock<AuthenticationSettings>();
            _webHost = new Mock<IWebHostEnvironment>();

            accountService = new AccountService(_dbContext, _passwordHasher.Object, _authenticationSetting.Object, _webHost.Object);

        }

        [Fact]
        public async void Test1()
        {

            //Arrange

            var dto = new RegisterUserDto()
            {
                Email = "Test5@wp.pl",
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
    }
}
