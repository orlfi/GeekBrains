using AutoMapper;
using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;

namespace BankCards.ApiOrm.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Card, CardCreateRequest>().ReverseMap();

        CreateMap<Card, CardUpdateRequest>().ReverseMap();

        CreateMap<Card, CardResponse>().ReverseMap();
    }
}
