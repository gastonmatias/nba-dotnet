using System.ComponentModel.DataAnnotations;

namespace nba_dotnet.DTOs
{
    public class CreateTeamDTO
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string TeamName { get; set; }

        [StringLength(maximumLength:50)]
        public string Coach { get; set; }
        
        [Required]
        [StringLength(maximumLength:100)]
        public string City { get; set; }

        public int ConferenceId { get; set; }
    }
}