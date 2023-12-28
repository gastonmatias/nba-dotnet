using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nba_dotnet.DTOs;

namespace nba_dotnet.Controllers
{
    [ApiController]
    [Route("api/conferences")]
    public class ConferenceController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly IMapper mapper;

        public ConferenceController(ApplicationDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool withTeams = false)
        {
            if (withTeams)
            {
                var conferences = await appDbContext.Conferences.Include(conferenceDb => conferenceDb.Teams).ToListAsync();
                var dtos = mapper.Map<List<ConferenceWithTeamsDTO>>(conferences);
                return Ok(new { status = "ok", data = dtos });
            }
            else
            {
                var conferences = await appDbContext.Conferences.ToListAsync();
                var dtos = mapper.Map<List<ConferenceDTO>>(conferences);
                return Ok(new { status = "ok", data = dtos });
            }
        }

    }
}