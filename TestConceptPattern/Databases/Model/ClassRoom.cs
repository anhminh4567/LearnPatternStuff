using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestConceptPattern.Databases.Model
{
	public class ClassRoom
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public int RoomNumber { get; set; }
		public List<Student> Students { get; set; }
		public List<JoinClassRoom> JoinClasses { get; set; }
	}
}
