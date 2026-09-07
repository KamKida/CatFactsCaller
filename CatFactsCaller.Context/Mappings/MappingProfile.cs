using AutoMapper;
using CatFactsCaller.Contracts.Dtos.Responses;
using CatFactsCaller.Domain.Models;

namespace CatFactsCaller.Context.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CatFact mapping
			CreateMap<CatFactResponse, CatFact>();
		}
	}
}