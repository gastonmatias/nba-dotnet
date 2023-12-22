using AutoMapper;
using nba_dotnet.DTOs;
using nba_dotnet.Entities;

namespace nba_dotnet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //* indicar en <> 1) fuente, 2) destino -------

            CreateMap<Player, PlayerDTO>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd-MM-yyyy")))
            ;

            CreateMap<CreatePlayerDTO, Player>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
            ;

            CreateMap<UpdatePlayerDTO, Player>();                
                
        }
    }
}