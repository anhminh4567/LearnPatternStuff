using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace XunitTesting.Mocking1
{
	public class MockDemo
	{



		public MockDemo()
		{
		}
		private readonly Mock<IUnitOfWork> _unitOfWork = new();
		[Fact]
		public void TestMock()
		{
			//arrange
			var newStudent = new Student();
			newStudent.Name = "minh";
			newStudent.Dob = DateTime.UtcNow;
			var moqObject = _unitOfWork.Object;

			//action
			var creatResult = moqObject.Repositories.StudentRepository.Create(newStudent);
			//assert
			_unitOfWork.Verify(x => x.Repositories.StudentRepository.Create(It.IsAny<Student>()), "create student is not called");
			Assert.NotNull(creatResult);
		}
		public void TestMockDeleteStudent()
		{
			//arrange
			var newStudent = new Student();
			newStudent.Name = "minh";
			newStudent.Dob = DateTime.UtcNow;
			_unitOfWork.Setup(u => u.Repositories.StudentRepository.Delete(It.IsAny<Student>())).Returns(It.IsAny<Student>());
			var moqObject = _unitOfWork.Object;
			//action
			var creatResult = moqObject.Repositories.StudentRepository.Create(newStudent);
			//assert
			_unitOfWork.Verify(x => x.Repositories.StudentRepository.Delete(It.IsAny<Student>()),
							Times.Once() ,
							"create student is not called");
			Assert.NotNull(creatResult);
		}
		
	}
}
