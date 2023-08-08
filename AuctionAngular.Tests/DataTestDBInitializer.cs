using Database;
using Database.Entities;

namespace AuctionAngularTests
{
    public class DataTestDBInitializer
    {
        public DataTestDBInitializer()
        {
        }

        public void Seed(AuctionDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Roles.AddRange(
                 new Role()
                 {
                     Name = "Buyer"
                 }
                );

            context.Users.AddRange(
                new User() {
                    Email = "Test2@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture="",
                    EmialConfirmed = false
                },
                new User()
                {
                    Email = "Test2@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    EmialConfirmed = true
                },
                new User()
                {
                    Email = "Test7@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    EmialConfirmed = false
                },
                new User()
                {
                    Email = "Test9@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    EmialConfirmed = true
                },
                new User()
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
                },
                new User()
                {
                    Email = "Test11@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    EmialConfirmed = true
                }
            );


            context.SaveChanges();
        }
    }
}

