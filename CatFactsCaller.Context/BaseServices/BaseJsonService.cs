using System.Text.Json;

namespace CatFactsCaller.Context.BaseServices
{
	public class BaseJsonService
	{
		public T Deserialize<T>(string json)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			return JsonSerializer.Deserialize<T>(json, options);
		}

	}
}
