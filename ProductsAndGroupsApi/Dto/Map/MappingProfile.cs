using AutoMapper;
using ProductsAndGroupsApi.Db;

namespace ProductsAndGroupsApi.Dto.Map
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<ProductGroupDto,ProductGroup>().ReverseMap();
        }
    }
}
