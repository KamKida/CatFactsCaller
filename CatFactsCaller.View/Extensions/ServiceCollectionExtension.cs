using CatFactsCaller.Context.BaseServices;
using CatFactsCaller.Context.Interfaces;
using CatFactsCaller.Context.Mappings;
using CatFactsCaller.Context.Services;
using CatFactsCaller.View.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CatFactsCaller.View.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddLogging(logging =>
			{
				logging.ClearProviders();
				logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
				logging.AddNLog();
			});

			//views
			services.AddScoped<MainMenu>();

			//baseservice
			services.AddSingleton<BaseHttpService>();
			services.AddSingleton<BaseJsonService>();
			services.AddScoped<BaseFileService>();


			//services
			services.AddScoped<ICatFactService, CatFactService>();

			//mapper
			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});

			return services;
		}
	}
}