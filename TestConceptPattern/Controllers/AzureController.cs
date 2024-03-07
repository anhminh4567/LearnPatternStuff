using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestConceptPattern.Services;

namespace TestConceptPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureController : ControllerBase
    {
        private readonly AzureBlobServices _azureBlobService;

        public AzureController(AzureBlobServices azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok( _azureBlobService.ListAsync());
        }
        [HttpGet("stream")]
        public async Task<IActionResult> Stream()
        {
            await _azureBlobService.StreamAudio("HongKong1[DEMO] - Nguyễn Trọng Tài.wav",HttpContext);
            Console.WriteLine("finish stream part");
            return Empty;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await _azureBlobService.UploadFile(file);
            return Ok();
        }
        [HttpPost("download")]
        
        public async Task<IActionResult> Download([FromForm]string filename)
        {
            var result = await _azureBlobService.DownloadFile(filename);
            return File(result.Value.stream,result.Value.contentType);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] string blobFileName)
        {
            await _azureBlobService.DeleteFile(blobFileName);
            return Ok();
        }
    }
}
