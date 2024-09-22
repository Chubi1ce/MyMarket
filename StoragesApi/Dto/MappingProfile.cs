using AutoMapper;
using StoragesApi.Db;

namespace StoragesApi.Dto
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            //если все поля совпадают один в один
            //CreateMap<StorageDto,Storage>().ReverseMap();

            CreateMap<StorageDto, Storage>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Description))
                .ReverseMap();
        }
    }
}
