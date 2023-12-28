using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nba_dotnet.DTOs;
using nba_dotnet.Entities;

namespace nba_dotnet.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly IMapper mapper;

        public TeamController(
            ApplicationDbContext appDbContext, IMapper mapper
        )
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        //! GET ALL TEAMS -----------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Get(){

            var teams = await appDbContext.Teams
                        .Include(teamDb => teamDb.Conference)
                        .ToListAsync();

            var dtos = mapper.Map<List<TeamDTO>>(teams);
            
            return Ok( new {status = "ok", data = dtos} );
        }
        
        //! GET TEAM BY ID -----------------------------------------------------------------------
        [HttpGet("{id:int}", Name = "getTeamById")]
        public async Task<ActionResult<TeamDTO>> GetById([FromRoute] int id){

            var team = await appDbContext.Teams
                        .Include(teamDb => teamDb.Conference)
                        .FirstOrDefaultAsync(teamDb => teamDb.Id == id);

            var dto = mapper.Map<TeamDTO>(team);
            
            return Ok( new {status = "ok", data = dto} );
        }

        //! CREATE TEAM  -----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<TeamDTO>> Post([FromBody] CreateTeamDTO createTeamDTO){

            var existsConference = await appDbContext.Conferences.AnyAsync(x=> x.Id == createTeamDTO.ConferenceId);

            if (!existsConference)
            {
                return BadRequest(new{status="error", message="The specified ConferenceId does not exist in the database! "});
            }

            var newTeam = mapper.Map<Team>(createTeamDTO);

            appDbContext.Add(newTeam);

            await appDbContext.SaveChangesAsync();

            var teamDTO = mapper.Map<TeamDTO>(newTeam);

            var response = new
            {
                message = "Created",
                data = teamDTO 
            };

            return CreatedAtRoute( "getTeamById",  new { id = newTeam.Id }, response );
        }
        
        //! UPDATE TEAM  -----------------------------------------------------------------------
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TeamDTO>> Put([FromRoute] int id,[FromBody] UpdateTeamDTO updateTeamDTO){

            // validar existencia de team
            var existsTeam = await appDbContext.Teams.AnyAsync(x=> x.Id == id);
            
            if (!existsTeam)
            {
                return BadRequest(new{status="error", message="The specified TeamId does not exist in the database! "});
            }
            
            // validar existencia de conferencia
            var existsConference = await appDbContext.Conferences.AnyAsync(x=> x.Id == updateTeamDTO.ConferenceId);

            if (!existsConference)
            {
                return BadRequest(new{status="error", message="The specified ConferenceId does not exist in the database! "});
            }

            var teamToUpdate = mapper.Map<Team>(updateTeamDTO);

            appDbContext.Add(teamToUpdate);

            await appDbContext.SaveChangesAsync();

            var teamDTO = mapper.Map<TeamDTO>(teamToUpdate);

            var response = new
            {
                message = "Updated",
                data = teamDTO 
            };

            return CreatedAtRoute( "getTeamById",  new { id = teamToUpdate.Id }, response );
        }

        //! DELETE TEAM  -----------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){

            var existsTeam = await appDbContext.Teams.AnyAsync(x => x.Id == id);

            if (!existsTeam)
            {
                return BadRequest( new {status="error", message="The specified TeamId does not exist in the database!"} );
            }
            
            //... 
            appDbContext.Remove( new Team{Id = id});

            await appDbContext.SaveChangesAsync();

            var response = new {
                status = "ok",
                message = "Team Deleted!"
            };

            return Ok(response);
        }
        
    }
}