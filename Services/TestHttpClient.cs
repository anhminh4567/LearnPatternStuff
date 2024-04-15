using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class TestHttpClient
	{
		private readonly HttpClient _httpClient;

		public TestHttpClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<string> FetchGet() 
		{
			return await _httpClient.GetFromJsonAsync<string>("/api");
		}

	}
}
