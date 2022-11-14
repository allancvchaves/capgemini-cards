using AutoMapper;
using CapgeminiCard.API.DTO;
using CapgeminiCard.API.Entities;

namespace CapgeminiCard.API.Mapper
{
    public class CardMapping: Profile
    {
        public CardMapping()
        {
            CreateMap<Card, GenerateCardDTO>().ReverseMap();
            CreateMap<Card, ValidateCardDTO>().ReverseMap();
        }
    }
}
