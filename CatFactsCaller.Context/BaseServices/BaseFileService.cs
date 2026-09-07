using CatFactsCaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatFactsCaller.Context.BaseServices
{
	public class BaseFileService
	{
		private readonly string _filePath;

		public BaseFileService()
		{
			var directory = Path.GetFullPath(
				Path.Combine(
					AppContext.BaseDirectory,
					"..",
					"..",
					"..",
					".."));

			_filePath = Path.Combine(
				directory,
				"catfacts.txt");
		}

		public async Task Save(CatFact catFact)
		{
			var line = $"{catFact.Fact} | Length: {catFact.Length}\n" +
					   $"-------------------------------------------------------------------------------\n" +
					   $"\n";

			await File.AppendAllTextAsync(
				_filePath,
				line + Environment.NewLine);
		}
	}
}
