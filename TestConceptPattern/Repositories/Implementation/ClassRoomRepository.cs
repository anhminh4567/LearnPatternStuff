using TestConceptPattern.Databases;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces;

namespace TestConceptPattern.Repositories.Implementation
{
	public class ClassRoomRepository : RepositoryBase<ClassRoom>, IClassRoomRepository
	{
		public ClassRoomRepository(SchoolDbContext context) : base(context)
		{
		}
	}
}
