using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;
using Wanaheim.Mapping.Dtos;

namespace FloatingMarket.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDto, Item>();

            CreateMap<Item, ItemDtoReadOnly>()
                .ForMember(i => i.Category,
                    o => o.MapFrom(c => new KeyValuePairDto { Id = c.Subcategory.CategoryId, Name = c.Subcategory.Category.Name }));

            CreateMap<Item, ItemDto>();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Subcategory, KeyValuePairDto>()
                .ReverseMap();


            CreateMap<ItemsQuery, ItemsQueryDto>()
                .ReverseMap();

            CreateMap<Photo, PhotoDto>()
               .ReverseMap();

            CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>));

            CreateMap<SignUpDto, AppUser>()
                .ForMember(a => a.UserName, o => o.MapFrom(s => s.Email));

            CreateMap<Player, PlayerDto>()
                .ReverseMap();
        }
    }
}
