using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestConceptPattern.Databases;
using TestConceptPattern.Databases.Model;

namespace TestConceptPattern.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly TestConceptPattern.Databases.SchoolDbContext _context;

        public IndexModel(TestConceptPattern.Databases.SchoolDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Students != null)
            {
                Student = await _context.Students.ToListAsync();
            }
        }
    }
}
