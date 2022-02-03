using AutoMapper;
using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;

namespace BankCards.ApiOrm.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Card, CardCreateRequest>();
        CreateMap<CardCreateRequest, Card>();

        CreateMap<Card, CardUpdateRequest>();
        CreateMap<CardUpdateRequest, Card>();

        CreateMap<Card, CardResponse>();
        CreateMap<CardResponse, Card>();
    }
}
