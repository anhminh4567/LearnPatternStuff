using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestConceptPattern.Caching;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace TestConceptPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachesController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IStudentRepository _studentRepository;
        public CachesController(ICacheService cacheService, IStudentRepository studentRepository)
        {
            _cacheService = cacheService;
            _studentRepository = studentRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> CacheGetAll()
        {
            var getStudent = await _cacheService.GetAsync<IEnumerable<Student>>("student_all");
            if (getStudent is not null)
                return Ok(getStudent);
            getStudent = _studentRepository.FindAll();
            await _cacheService.SetAsync("student_all", getStudent);
            return Ok(getStudent);
        }
    }
}
