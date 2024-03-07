using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces;

namespace TestConceptPattern.CQRS
{
	public class CreateStudentCommand : IRequest<Student>
	{
		public DateTime Dob { get;set; }
		public string? Name { get; set; }
		public List<ClassRoom> Classrooms { get; set; }
	}
	public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Student>
	{
		private readonly IStudentRepository _studentRepository;
		private readonly IClassRoomRepository _classRoomRepository;

		public CreateStudentHandler(IStudentRepository studentRepository, IClassRoomRepository classRoomRepository)
		{
			_studentRepository = studentRepository;
			_classRoomRepository = classRoomRepository;
		}

		public Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
		{
			var newStudent = new Student()
			{
				Dob = request.Dob,
				Name = request.Name,
				ClassRooms = request.Classrooms,
			};
			var createResult = _studentRepository.Create(newStudent);
			return Task.FromResult(createResult);
		}
	}
}
