using CatFactsCaller.Context.Interfaces;
using CatFactsCaller.Context.Services;
using CatFactsCaller.View.Extensions;
using CatFactsCaller.View.Views;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddServices();

var serviceProvider = services.BuildServiceProvider();

MainMenu mainMenu = serviceProvider.GetRequiredService<MainMenu>();

await mainMenu.ShowMenu();