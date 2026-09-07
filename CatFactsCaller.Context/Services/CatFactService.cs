using AutoMapper;
using CatFactsCaller.Context.BaseServices;
using CatFactsCaller.Context.Interfaces;
using CatFactsCaller.Context.Mappings;
using CatFactsCaller.Contracts.Dtos.Responses;
using CatFactsCaller.Domain.Models;
using Microsoft.Extensions.Logging;
using NLog;
using System.Threading.Tasks;

namespace CatFactsCaller.Context.Services
{
	public class CatFactService : ICatFactService
	{
		private readonly BaseHttpService _baseHttpService;
		private readonly BaseJsonService _baseJsonService;
		private readonly IMapper _mapper;
		private readonly ILogger<CatFactService> _logger;


		public CatFactService(
			BaseHttpService baseHttpService,
			BaseJsonService baseJsonService,
			IMapper mapper,
			ILogger<CatFactService> logger)
		{
			_baseHttpService = baseHttpService;
			_baseJsonService = baseJsonService;
			_mapper = mapper;
			_logger = logger;
		}



		public async Task<CatFact?> Get()
		{
			try
			{
				_logger.LogInformation("Sending request to Cat Facts API.");

				HttpResponseMessage response =
					await _baseHttpService._httpClient.GetAsync("");

				_logger.LogInformation(
					"Received response from Cat Facts API. Status code: {StatusCode}",
					response.StatusCode);

				if (!response.IsSuccessStatusCode)
				{
					_logger.LogWarning(
						"Cat Facts API returned unsuccessful status code: {StatusCode}",
						response.StatusCode);

					return null;
				}

				var json = await response.Content.ReadAsStringAsync();

				_logger.LogDebug(
					"Received response content: {ResponseContent}",
					json);

				var catFactRespopnse = _baseJsonService.Deserialize<CatFactResponse>(json);

				if (catFactRespopnse == null)
				{
					_logger.LogWarning(
						"Failed to deserialize Cat Facts API response.");

					return null;
				}

				_logger.LogInformation(
					"Successfully retrieved cat fact. Length: {Length}",
					catFactRespopnse.Length);


					CatFact catFact = _mapper.Map<CatFact>(catFactRespopnse);

					return catFact;
			}
			catch (HttpRequestException ex)
			{
				_logger.LogError(
					ex,
					"HTTP error occurred while calling Cat Facts API.");

				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Unexpected error occurred while getting cat fact.");

				return null;
			}
		}
	}
}
