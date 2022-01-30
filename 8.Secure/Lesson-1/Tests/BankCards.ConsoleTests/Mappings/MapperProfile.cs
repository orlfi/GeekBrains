using AutoMapper;
using BankCards.ConsoleTests.DTO.Cards;
using BankCards.Domain;

namespace BankCards.ApiOrm.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Card, CardResponse>();
        CreateMap<CardResponse, Card>();
    }
}
