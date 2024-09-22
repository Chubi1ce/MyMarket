using AutoMapper;
using PlacementApi.Db;

namespace PlacementApi.Dto
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<PlacementDto,Placement>().ReverseMap();
        }
    }
}
