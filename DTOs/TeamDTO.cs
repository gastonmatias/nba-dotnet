
namespace nba_dotnet.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public int ConferenceId { get; set; }
    }
}