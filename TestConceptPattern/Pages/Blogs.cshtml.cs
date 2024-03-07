using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace TestConceptPattern.Pages
{
    public class BlogsModel : PageModel
    {
		[BindProperty]
		public List<string>? TextContents { get; set; }

		[BindProperty]
		public List<IFormFile>? ImageContents { get; set; }

		public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            return Page();
        }
    }
    public class BlogDto
    {

    }
}
