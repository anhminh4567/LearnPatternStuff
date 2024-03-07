using MediatR;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces;

namespace TestConceptPattern.CQRS
{
	public class QueryAllStudents : IRequest<List<Student>>
	{
	}
	public class QueryAllStudentsHandler : IRequestHandler<QueryAllStudents, List<Student>>
	{
		private readonly IStudentRepository _studentRepository;

		public QueryAllStudentsHandler(IStudentRepository studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public Task<List<Student>> Handle(QueryAllStudents request, CancellationToken cancellationToken)
		{
			var getAll = _studentRepository.FindAll().ToList();
			return Task.FromResult(getAll);
		}
	}
}
