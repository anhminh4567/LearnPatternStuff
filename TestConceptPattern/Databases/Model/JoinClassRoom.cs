namespace TestConceptPattern.Databases.Model
{
	public class JoinClassRoom
	{
		public int ClassId { get; set; }
		public ClassRoom ClassRoom { get; set; }
		public int StudentId { get; set; }
		public Student Student { get; set; }
		public DateTime JoinDate { get; set; }
	}
}
