using AutoMapper;
using VillaWebApp.Models;
using VillaWebApp.Models.Dto;

namespace VillaWebApp
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaCreateDTO>().ReverseMap();
        }
    }
}
