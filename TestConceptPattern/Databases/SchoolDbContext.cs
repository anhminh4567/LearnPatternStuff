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
			//			optionsBuilder.UseSqlServer("server =(local);uid=sa;pwd=12345;Database=LearnCSharpEFCore;TrustServerCertificate=true;");
			string serverName = "localhost,7878";
			string uid = "SA";
			string pwd = "Supermarcy@2003";
			string database = "LearnCSharpEFCore";
            try {
				var env_servername = Environment.GetEnvironmentVariable("MYAPP_SERVERNAME");
                var env_uid = Environment.GetEnvironmentVariable("MYAPP_UID");
                var env_pwd = Environment.GetEnvironmentVariable("MYAPP_PWD");
                var env_database = Environment.GetEnvironmentVariable("MYAPP_DATABASE");
				if ( string.IsNullOrEmpty(env_servername)||
					string.IsNullOrEmpty(env_uid) ||
					string.IsNullOrEmpty(env_pwd) || 
					string.IsNullOrEmpty(env_database) ) 
				{
					throw new ArgumentException("empty environment variable, take default variable");
				}
				serverName = env_servername;
				uid = env_uid;
				pwd = env_pwd;
				database = env_database;
            }
            catch(Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}

			var connectionString = $"server={serverName};uid={uid};pwd={pwd};Database={database};TrustServerCertificate=true;";
			optionsBuilder.UseSqlServer(connectionString) ;
			//optionsBuilder.UseSqlServer("server=localhost,7878;uid=SA;pwd=Supermarcy@2003;Database=LearnCSharpEFCore;TrustServerCertificate=true;");

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
