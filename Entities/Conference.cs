using System.ComponentModel.DataAnnotations;

namespace nba_dotnet.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(maximumLength:50)]
        public string ConferenceName { get; set; }

        // Propiedad de navegación a los equipos de la conferencia
        public ICollection<Team> Teams { get; set; }        
    }
}