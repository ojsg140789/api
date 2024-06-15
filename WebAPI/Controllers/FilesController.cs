using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileName = file.FileName;
            var fileExtension = Path.GetExtension(fileName);
            var fileSize = file.Length;

            _logger.LogInformation("File uploaded: Name={FileName}, Extension={FileExtension}, Size={FileSize} bytes",
                fileName, fileExtension, fileSize);

            var fileInfo = new
            {
                Name = fileName,
                Extension = fileExtension,
                Size = fileSize
            };

            return Ok(fileInfo);
        }
    }
}