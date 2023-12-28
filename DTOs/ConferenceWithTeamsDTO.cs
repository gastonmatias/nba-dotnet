
namespace nba_dotnet.DTOs
{
    public class ConferenceWithTeamsDTO
    {
        public int Id { get; set; }
        public string ConferenceName { get; set; }
        public List<TeamsInConferenceDTO> Teams { get; set; }
    }
}