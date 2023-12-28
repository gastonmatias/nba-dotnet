using AutoMapper;
using nba_dotnet.DTOs;
using nba_dotnet.Entities;

namespace nba_dotnet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //* indicar en <>... < source, destiny > -------

            //! PLAYER ---------------------------------------------------------------------------------------------
            CreateMap<Player, PlayerDTO>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd-MM-yyyy")));

            CreateMap<CreatePlayerDTO, Player>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)));

            CreateMap<UpdatePlayerDTO, Player>();                

            //! TEAM ---------------------------------------------------------------------------------------------
            CreateMap<Team,TeamDTO>();
            CreateMap<CreateTeamDTO,Team>();
            CreateMap<UpdateTeamDTO,Team>();

            //! CONFERENCE ----------------------------------------------------------------------------------------
            CreateMap<Conference, ConferenceDTO>();
            
            CreateMap<Conference, ConferenceWithTeamsDTO>()
                .ForMember(dest => dest.Teams, opts => opts.MapFrom( src => 

                    src.Teams.Select(team => new TeamsInConferenceDTO {
                        Id = team.Id,
                        TeamName = team.TeamName,
                        City = team.City
                    }).ToList()
                
                ))
            ;
        }
    }
}