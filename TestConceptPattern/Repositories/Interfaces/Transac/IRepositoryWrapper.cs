namespace TestConceptPattern.Repositories.Interfaces.Transac
{
    public interface IRepositoryWrapper
    {
        IClassRoomRepository ClassRoomRepository { get; }
        IStudentRepository StudentRepository { get; }
    }
}
