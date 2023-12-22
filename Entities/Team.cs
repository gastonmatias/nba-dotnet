using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nba_dotnet.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:100)]
        public string TeamName { get; set; }

        [StringLength(maximumLength:50)]
        public string Coach { get; set; }
        
        [Required]
        [StringLength(maximumLength:100)]
        public string City { get; set; }

        public int ConferenceId { get; set; }

        // Propiedad de navegación a la conferencia
        public Conference Conference { get; set; }

        // Propiedad de navegación a los jugadores del equipo
        public List<Player> Players { get; set; }
    }

}