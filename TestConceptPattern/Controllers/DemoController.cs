using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestConceptPattern.CQRS;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Dto;

namespace TestConceptPattern.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DemoController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public DemoController(IMediator mediator , IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllStudents() 
		{
			var getAllQuery = new QueryAllStudents();
			var result = await _mediator.Send(getAllQuery);
			IList<StudentDto> returnResult = new List<StudentDto>();
			foreach (var student in result) 
			{
				returnResult.Add( _mapper.Map<StudentDto>(student));
			}
			return Ok(returnResult);
		}
		[HttpPost]
		public async Task<IActionResult> CreateStudent( StudentDto student,CancellationToken cancellationToken)
		{
			var createCommand = new CreateStudentCommand()
			{
				Dob = student.Dob.Value,
				//Classrooms = student.ClassRooms,
				Name = student.Name,
			};
			var result = await _mediator.Send(createCommand, cancellationToken);
			return Ok(result);
		}
		
	}

}
