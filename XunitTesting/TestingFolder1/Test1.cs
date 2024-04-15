using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConceptPattern.Repositories.Implementation.Transac;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace XunitTesting.TestingFolder1
{
	public class Test1
	{
		private readonly IUnitOfWork _unitOfWork;
		[Fact]
		public void TestInsert()
		{
			//arrangee (set param, mock )

			//act ( action )

			//assertion ( validation )
		}
		[Fact]
		public void TestSampleFunction()
		{
			// arrrange
			var testFunction = new TestFunction();
			
			// action
			var newname = testFunction.Add("minh");
			var getName = testFunction.Get("minh");
			
			//assertion
			Assert.NotNull(newname);
			Assert.Equal("minh",getName);
		}
		[Fact]
		public void TestSampleFunction2()
		{
			// arrange
			var testFunction = new TestFunction();
			testFunction.Add("1");
			testFunction.Add("2");
			//action

			testFunction.Add("asdf");
			//testFunction.Reemove("asdf");

			var get = testFunction.Get("asdf");
			// assertion
			//Assert.Null(get);
			Assert.Multiple(
				() => { 
					Assert.Null(get); 
				}, 
				() => {
					Assert.Equal((decimal)1, (decimal)testFunction.username.Count, 1);
				});
		}

	}
}
