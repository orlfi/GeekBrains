using AutoMapper;
using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;
using BankCards.Domain.Mongo;

namespace BankCards.ApiOrm.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Card, CardCreateRequest>().ReverseMap();

        CreateMap<Card, CardUpdateRequest>().ReverseMap();

        CreateMap<Card, CardResponse>().ReverseMap();

        CreateMap<Book, BookCreateRequest>().ReverseMap();

        CreateMap<Book, BookUpdateRequest>().ReverseMap();

        CreateMap<Book, BookResponse>().ReverseMap();

    }
}
