using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestConceptPattern.Databases.Model
{
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime? Dob { get; set; }
		public List<ClassRoom>? ClassRooms { get; set; }
		public List<JoinClassRoom>? JoinClasses { get; set; }
	}
}
