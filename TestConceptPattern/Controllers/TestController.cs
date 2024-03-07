using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestConceptPattern.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		public TestController()
		{
		}
		[HttpGet]
		//[Produces("application/json")]
		public async Task<IActionResult> Test() 
		{
			//HttpContext.Response.Headers.Add("Content-Type", "application/json");
			return new JsonResult(new { StatusCode = 200, Message = "yea shit work" });
		}
	}
}
