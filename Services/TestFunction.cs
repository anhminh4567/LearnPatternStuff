using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class TestFunction
	{
		public List<string> username { get;set; }

		public TestFunction()
		{
			this.username = new List<string>();
		}
		public string Add(string namee)
		{
			username.Add(namee);
			return username.First(u => u.Equals(namee));
		}
		public string Get(string namee) 
		{
			return username.First(u => u.Equals(namee));
		}
		public bool Reemove(string namee)
		{
			return username.Remove(namee);
		}
	}
}
