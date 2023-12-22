using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nba_dotnet.Entities
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(maximumLength:50)]
        public string LastName { get; set; }
        
        [Column(TypeName = "decimal(3,2)")]
        public decimal Height { get; set; }
        
        [Column(TypeName = "decimal(5,1)")]
        public decimal Weight { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(maximumLength:50)]
        public string Position { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal PointsPerGame { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal ReboundsPerGame { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal AssistsPerGame { get; set; }

        public int TeamId { get; set; }

        // prop navegacion a Team
        public Team Team { get; set; }
    }
}