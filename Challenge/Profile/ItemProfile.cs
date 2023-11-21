using AutoMapper;
using Challenge.DTO;
using Challenge.Entity;

namespace Challenge.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();
        }
    }
}
