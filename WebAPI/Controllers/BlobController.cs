using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Munizoft.Azure.Services;
using Munizoft.Azure.Services.Resources;
using System;
using System.Threading.Tasks;

namespace Munizoft.Azure.BlogService.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : ControllerBase
    {
        #region Fields
        private readonly ILogger<BlobController> _logger;
        private readonly Munizoft.Azure.Services.IBlobService _blobService;
        #endregion Fields

        #region Constructor
        public BlobController(
            ILogger<BlobController> logger,
            IBlobService blobService
            )
        {
            _logger = logger;
            _blobService = blobService;
        }
        #endregion Constructor

        [HttpGet("{container}/{blobName}")]
        public async Task<IActionResult> Get(String container, String blobName)
        {
            _logger.LogInformation("Begin getting blob");
            var result = await _blobService.GetBlobAsync(container, blobName);
            _logger.LogInformation("End getting blob");

            return Ok(result);
        }

        [HttpGet("download/{container}/{blobName}")]
        public async Task<IActionResult> Download(String container, String blobName)
        {
            _logger.LogInformation("Begin downloading blob");
            var result = await _blobService.DownloadAsync(container, blobName);
            _logger.LogInformation("End downloading blob");

            return Ok(result);
        }

        [HttpGet("{container}")]
        public async Task<IActionResult> List(String container)
        {
            _logger.LogInformation("Begin listing blobs");
            var result = await _blobService.ListBlobsAsync(container);
            _logger.LogInformation("End listing blobs");

            return Ok(result);
        }

        [HttpPost("Upload/File")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            _logger.LogInformation("Begin uploading file");
            await _blobService.UploadFileAsync(request.Container, request.FilePath, request.Filename);
            _logger.LogInformation("End uploading file");

            return Ok(true);
        }

        [HttpPost("Upload/Content")]
        public async Task<IActionResult> UploadContent([FromBody] UploadContentRequest request)
        {
            _logger.LogInformation("Begin uploading content");
            await _blobService.UploadContentAsync(request);
            _logger.LogInformation("End uploading content");

            return Ok(true);
        }

        [HttpDelete("{container}/{blobName}")]
        public async Task<IActionResult> DeleteFile(String container, String blobName)
        {
            _logger.LogInformation("Begin deleting file");
            await _blobService.DeleteBlobAsync(container, blobName);
            _logger.LogInformation("End deleting file");

            return Ok(true);
        }
    }
}