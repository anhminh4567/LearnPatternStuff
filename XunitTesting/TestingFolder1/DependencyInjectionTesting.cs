using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace XunitTesting.TestingFolder1
{
	public class DependencyInjectionTesting
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public DependencyInjectionTesting(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[Fact]
		public void TestInsertStudent()
		{
			//arrange
			//var newStudent = new Student();
			//newStudent.Name = "minh";
			//newStudent.Dob = DateTime.UtcNow;

			//action
			//var newResult = ( _unitOfWork.Repositories.StudentRepository.Create(newStudent));
			
			//assertion
			//Assert.NotNull(newResult);
		}

	}
}
