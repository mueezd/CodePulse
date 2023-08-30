using CodePulse.API.Models.DomainModels;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        // POST:  {apibaseurl}/api/images

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string filaName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var blohImage = new BlogImage
                {
                    FileExtention = Path.GetExtension(file.FileName).ToLower(),
                    FileName = filaName,
                    Title = title,
                    DateCreated = DateTime.Now
                };

                blohImage = await _imageRepository.Upload(file, blohImage);

                // COnvter Domain Model to DTO

                var response = new BlogImageDto
                {
                    Id = blohImage.Id,
                    Title = blohImage.Title,
                    DateCreated = blohImage.DateCreated,
                    FileExtention = blohImage.FileExtention,
                    FileName = blohImage.FileName,
                    Url = blohImage.Url
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtention = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtention.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file foemate");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot mor then 10 mb");

            }
        }

    }
}
