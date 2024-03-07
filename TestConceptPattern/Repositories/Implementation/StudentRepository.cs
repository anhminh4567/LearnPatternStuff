using TestConceptPattern.Databases;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces;

namespace TestConceptPattern.Repositories.Implementation
{
	public class StudentRepository : RepositoryBase<Student>, IStudentRepository
	{
		public StudentRepository(SchoolDbContext context) : base(context)
		{
		}
	}
}
