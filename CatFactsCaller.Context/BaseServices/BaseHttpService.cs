namespace CatFactsCaller.Context.BaseServices
{
	public class BaseHttpService
	{
		public HttpClient _httpClient;

		public BaseHttpService()
		{
			_httpClient = new HttpClient()
			{
				BaseAddress = new Uri("https://catfact.ninja/fact")
			};
		}
	}
}
