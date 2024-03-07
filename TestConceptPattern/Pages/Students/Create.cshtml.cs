using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using TestConceptPattern.Databases;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace TestConceptPattern.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        [Required]
        public string Name { get; set; } = default!;
        [BindProperty]
        [Required]
        public DateTime Dob { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _unitOfWork.BeginTransaction();
                var resultStudent1 = _unitOfWork.Repositories.StudentRepository.Create(new Student()
                {
                    Name = Name,
                    Dob = Dob,
                });
                await _unitOfWork.SaveChangesAsync();
                var resultStudent2 = _unitOfWork.Repositories.StudentRepository.Create(new Student()
                {
                    Name = Name + " clone",
                    Dob = Dob,
                });
                await _unitOfWork.SaveChangesAsync();
                throw new Exception(resultStudent1.ToString());
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                await _unitOfWork.RollBackAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
