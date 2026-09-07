using CatFactsCaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatFactsCaller.Context.Interfaces
{
	public interface ICatFactService 
	{
		Task<CatFact> Get();
	}
}
