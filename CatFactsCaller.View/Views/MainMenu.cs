using CatFactsCaller.Context.BaseServices;
using CatFactsCaller.Context.Interfaces;
using CatFactsCaller.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CatFactsCaller.View.Views
{
	public class MainMenu
	{
		private readonly ICatFactService _catFactService;
		private readonly BaseFileService _fileService;
		private readonly ILogger<MainMenu> _logger;

		public MainMenu(
			ICatFactService catFactService,
			BaseFileService fileService,
			ILogger<MainMenu> logger)
		{
			_catFactService = catFactService;
			_fileService = fileService;
			_logger = logger;
		}

		public async Task ShowMenu()
		{
			_logger.LogInformation("Application started.");

			try
			{
				while (true)
				{
					Console.Clear();

					Console.WriteLine("Wyszukiwanie faktu o kocie...");
					Console.WriteLine();

					_logger.LogInformation(
						"Requesting a new cat fact.");

					CatFact? catFact = await _catFactService.Get();

					Console.Clear();

					if (catFact == null)
					{
						_logger.LogWarning(
							"Cat fact was not received from API.");

						Console.WriteLine(
							"Nie udało się pobrać faktu o kocie.");
					}
					else
					{
						_logger.LogInformation(
							"Cat fact successfully received. Length: {Length}",
							catFact.Length);

						Console.WriteLine("FAKT:");
						Console.WriteLine();
						Console.WriteLine(catFact.Fact);
						Console.WriteLine();
						Console.WriteLine($"Długość: {catFact.Length}");

						try
						{
							_logger.LogInformation(
								"Saving cat fact to file.");

						    await _fileService.Save(catFact);

							Console.WriteLine();
							Console.WriteLine(
								"Fakt został zapisany do pliku.");

							_logger.LogInformation(
								"Cat fact successfully saved to file.");
						}
						catch (Exception ex)
						{
							_logger.LogError(
								ex,
								"Failed to save cat fact to file.");

							Console.WriteLine();
							Console.WriteLine(
								"Nie udało się zapisać faktu do pliku.");
						}
					}

					Console.WriteLine();
					Console.WriteLine(
						"Naciśnij 'N', aby wyszukać nowy fakt.");
					Console.WriteLine(
						"Naciśnij dowolny inny klawisz, aby zakończyć.");

					var key = Console.ReadKey(true);

					_logger.LogInformation(
						"User pressed key: {Key}",
						key.Key);

					if (key.Key != ConsoleKey.N)
					{
						_logger.LogInformation(
							"Application terminated by user.");

						break;
					}

					_logger.LogInformation(
						"Starting next cat fact request.");
				}
			}
			catch (Exception ex)
			{
				_logger.LogCritical(
					ex,
					"Unexpected critical error in MainMenu.");

				Console.WriteLine();
				Console.WriteLine(
					"Wystąpił nieoczekiwany błąd aplikacji.");
			}

			_logger.LogInformation("Application finished.");
		}
	}
}