using AuctionAngular.Dtos;
using AuctionAngular.Dtos.MailDto;
using AuctionAngular.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace AuctionAngular.Tests
{
    public class MailServiceTest
    {
        private Mock<IOptions<MailSettings>> _mailSettings;

        private MailService mailService;


        public MailServiceTest()
        {

            _mailSettings = new Mock<IOptions<MailSettings>>();

            mailService = new MailService(_mailSettings.Object);
        }


        //[Fact]
        //public async Task SendEmailAsync_WithValidData_ShouldSendEmailAndReturnTrue()
        //{
        //    //Arrange

        //    var mailExaple = new MailRequestDto()
        //    {
        //        ToEmail = "JoeHeros@wp.pl",
        //        Subject = "Test",
        //        Body = "TestTest"
        //    };

        //    //Act

        //    var respons = await mailService.SendEmailAsync(mailExaple);

        //    //Assert

        //    Assert.True(respons);
        //}


        //[Fact]
        //public async Task SendEmailAsync_WithInvalidData_ShoulReturnFalse()
        //{
        //    //Arrange

        //    var mailExaple = new MailRequestDto()
        //    {
        //        ToEmail = "JoeHeros@wp.pl",
        //        Subject = "Test",
        //        Body = "TestTest"
        //    };

        //    //Act

        //    var respons = await mailService.SendEmailAsync(mailExaple);

        //    //Assert

        //    Assert.True(respons);
        //}
    }
}
