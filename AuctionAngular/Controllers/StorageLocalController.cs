using AuctionAngular.Dtos;
using AuctionAngular.Dtos.User;
using Azure.Storage.Blobs;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

namespace AuctionAngular.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StorageLocalController : ControllerBase
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;


        public StorageLocalController(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            this._dbContext = dbContext;
            this._webHost = webHost;
        }

        //[HttpGet("[action]")]
        //public async Task<ActionResult<Uri>> GetVehicleImage(string fileName)
        //{
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(containerVehicleImages);
        //    var blobClient = containerClient.GetBlobClient(fileName);

        //    return Ok(blobClient.Uri);
        //}


        //[HttpPost("[action]/{id}")]
        //public async Task<ActionResult<FileNameDto>> UploadVehicleImage([FromRoute] int id)
        //{
        //    IFormFile formFile = Request.Form.Files[0];
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(containerVehicleImages);
        //    var blobClient = containerClient.GetBlobClient(formFile.FileName);
        //    using (var stream = formFile.OpenReadStream())
        //    {
        //        await blobClient.UploadAsync(stream);
        //    }

        //    var fileName = new FileNameDto()
        //    {
        //        Name = formFile.FileName,
        //    };

        //    return Ok(fileName);
        //}




        [HttpGet("[action]/{fileName}")]
        public async Task<IActionResult> GetProfileImage([FromRoute] string fileName)
        {
            var imagePath = Path.Combine(_webHost.WebRootPath, "images", fileName);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg");
            }

            return NotFound();
        }





        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UploadProfileImage([FromRoute] int id)
        {
            IFormFile formFile = Request.Form.Files[0];

            string fileName = null;
            if (formFile != null)
            {
                string uploadDir = Path.Combine(_webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.ProfilePicture = fileName;

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
