using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nba_dotnet.DTOs;
using nba_dotnet.Entities;

namespace nba_dotnet.Controllers
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<PlayerController> logger;

        public PlayerController( ApplicationDbContext appDbContext, IMapper mapper, ILogger<PlayerController> logger)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        //! GET ALL PLAYERS ------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Get(){

            var players = await appDbContext.Players
                        .Include(playerDb => playerDb.Team)
                        .ThenInclude(teamDb => teamDb.Conference)
                        .ToListAsync();


            var dtos = mapper.Map<List<PlayerDTO>>(players); // Map<TDestination>(object source)

            return Ok( new {status = "ok", data = dtos} );
        }

        //! GET PLAYER BY ID ---------------------------------------------------------------------------------------------------
        [HttpGet("{id:int}", Name ="getPlayerById")]// con el name se puede hacer referencia a esta ruta en otros metodos
        public async Task<ActionResult<PlayerDTO>> GetById([FromRoute] int id){

            var player = await appDbContext.Players
                        .Include(PlayerDb => PlayerDb.Team)
                        .ThenInclude(teamDb => teamDb.Conference)
                        .FirstOrDefaultAsync(x => x.Id == id)
                        ;

            if (player == null) { return BadRequest(new {message = "no player found with the provided ID"}); }

            var dto = mapper.Map<PlayerDTO>(player); // Map<TDestination>(object source)

            return Ok( new {status = "ok", data = dto} );
        }

     
        //! CREATE PLAYER ------------------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePlayerDTO createPlayerDTO){

            //? Verificar si el TeamId pasado x body existe en la base de datos
            var team = await appDbContext.Teams
                            .Include(teamDb => teamDb.Conference)
                            .FirstOrDefaultAsync(teamDb => teamDb.Id == createPlayerDTO.TeamId);

            if (team==null) 
            {
                return BadRequest(new {
                    status = 400,
                    message = "The specified TeamId does not exist in the database!"
                }); 
            }
            
            //? mapeo del player dto extraido del body
            var newPlayer = mapper.Map<Player>(createPlayerDTO);
            
            //? Asignar prop teamId al player mapeado 
            //? (en esta caso basta con teamId del body para crear foreign key
            newPlayer.TeamId = team.Id;

            //? guardar entidad mapeada en bd
            appDbContext.Add(newPlayer);
            
            //? actualizar estado bd
            await appDbContext.SaveChangesAsync();
            
            //? mapeo entidad reecien creada para retornarla 
            var playerDTO = mapper.Map<PlayerDTO>(newPlayer);
            
            var response = new
            {
                message = "Created",
                data = playerDTO 
            };

            //! FUNCA otorgando correctamente el " location: http://localhost:5097/api/players/{id}"
            return CreatedAtRoute("getPlayerById", new { id = newPlayer.Id }, response);
        }

        //! UPDATE PLAYER ----------------------------------------------------------------------------
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdatePlayerDTO updatePlayerDTO){

            // buscar player con id pasado por route
            var exists = await appDbContext.Players.AnyAsync( x => x.Id == id);
            if (!exists) { return BadRequest(new {status= "Error", msje = "Player Not Found"});}

            // validar que exista team con teamId pasado por body
            var existsTeam = await appDbContext.Teams.AnyAsync(x => x.Id == updatePlayerDTO.TeamId);
            if (!existsTeam) { return BadRequest(new {status= "Error", msje = "Team Not Found"});}

            // mapear dto del formbody a entity
            var playerToUpdate = mapper.Map<Player>(updatePlayerDTO);
            
            // asignar la id qe viene desde route al entity
            playerToUpdate.Id = id;

            appDbContext.Update(playerToUpdate);

            await appDbContext.SaveChangesAsync();

            return NoContent();

        }
        
        //! DELETE PLAYER ----------------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete ([FromRoute] int id){

            var existsPlayer = await appDbContext.Players.AnyAsync( x => x.Id == id);
            if (!existsPlayer) { return BadRequest(new {status= "Error", msje = "Player Not Found"});}

            // NO se esta creando un nuevo player en bd, sino qe "se crea un objeto tipo player"
            // pq entity framework necesita una instancia para poder accionar
            appDbContext.Remove(new Player{Id = id});

            await appDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}

            