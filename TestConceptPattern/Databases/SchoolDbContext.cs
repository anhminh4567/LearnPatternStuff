using Microsoft.EntityFrameworkCore;
using TestConceptPattern.Databases.Model;

namespace TestConceptPattern.Databases
{
	public class SchoolDbContext : DbContext
	{
		public SchoolDbContext()
		{

		}

		public SchoolDbContext(DbContextOptions options) : base(options)
		{
		}

		
		public DbSet<Student> Students { get; set; }
		public DbSet<ClassRoom> ClassRooms { get; set; }
		public DbSet<JoinClassRoom> JoinClassRooms { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer("server =(local);uid=sa;pwd=12345;Database=LearnCSharpEFCore;TrustServerCertificate=true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Student>(s => 
			{
				s.HasMany(s => s.ClassRooms)
				.WithMany(c => c.Students)
				.UsingEntity<JoinClassRoom>(
					joinEntityName: "Join", 
						right => right.HasOne(j => j.ClassRoom).WithMany(c => c.JoinClasses).HasForeignKey(j => j.ClassId),
						left => left.HasOne( j => j.Student).WithMany(s => s.JoinClasses).HasForeignKey(j => j.StudentId)	
				);
			});
		}
	}
}
