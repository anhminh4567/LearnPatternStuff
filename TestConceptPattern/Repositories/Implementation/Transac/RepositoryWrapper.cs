
using TestConceptPattern.Repositories.Interfaces;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace TestConceptPattern.Repositories.Implementation.Transac
{
    // this is not necessary
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public IClassRoomRepository ClassRoomRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public RepositoryWrapper(IClassRoomRepository classRoomRepository, IStudentRepository studentRepository)
        {
            ClassRoomRepository = classRoomRepository;
            StudentRepository = studentRepository;
        }
    }
}
