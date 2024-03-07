using TestConceptPattern.Databases.Model;

namespace TestConceptPattern.Dto
{
	public class StudentDto
	{
		public string Name { get; set; }
		public DateTime? Dob { get; set; }
		public List<ClassRoomDto>? classRoomDtos { get; set; }
	}
	public class ClassRoomDto
	{
		public string Name { get; set; }
		public int RoomNumber { get; set; }
		public List<StudentDto>? Students { get; set; }

	}
}
