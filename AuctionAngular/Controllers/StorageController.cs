using AuctionAngular.Dtos;
using Azure.Storage.Blobs;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StorageController: ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AuctionDbContext _dbContext;
        private readonly string containerProfileImages = "profile-images";
        private readonly string containerVehicleImages = "vehicle-images";
        private readonly IConfiguration _configuration;


        public StorageController(IConfiguration configuration, AuctionDbContext dbContext)
        {
            string connectionAzure = "DefaultEndpointsProtocol=https;AccountName=storagevehicleauction;AccountKey=nTEomYfRiVp9WXbWzbttNwqKFPdNeBe4u1K0LknMvijAT9lrJincjkxWyGbXumsggBjY6wF8FTI3+AStiaw+Rw==;EndpointSuffix=core.windows.net";
            _blobServiceClient = new BlobServiceClient(connectionAzure);
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Uri>> GetVehicleImage(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerVehicleImages);
            var blobClient = containerClient.GetBlobClient(fileName);

            return Ok(blobClient.Uri);
        }


        [HttpPost("[action]/{id}")]
        public async Task<ActionResult<FileNameDto>> UploadVehicleImage([FromRoute] int id)
        {
            IFormFile formFile = Request.Form.Files[0];
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerVehicleImages);
            var blobClient = containerClient.GetBlobClient(formFile.FileName);
            using (var stream = formFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            var fileName = new FileNameDto()
            {
                Name = formFile.FileName,
            };

            return Ok(fileName);
        }




        [HttpGet("[action]")]
        public async Task<ActionResult<Uri>> GetProfileImage(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerProfileImages);
            var blobClient = containerClient.GetBlobClient(fileName);

            return Ok(blobClient.Uri);
        }


        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UploadProfileImage([FromRoute] int id)
        {
            IFormFile formFile = Request.Form.Files[0];

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);


            var containerClient = _blobServiceClient.GetBlobContainerClient(containerProfileImages);
            var blobClient = containerClient.GetBlobClient(uniqueFileName);
            using (var stream = formFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.ProfilePicture = uniqueFileName;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            var response = new { Message = "Upload successfully!" };
            return Ok(response);
        }
    }
}
