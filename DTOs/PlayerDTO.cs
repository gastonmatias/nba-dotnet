using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nba_dotnet.DTOs
{
    public class PlayerDTO
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
        
        // string para correcto parseo de fecha en automapperprofile
        public string DateOfBirth { get; set; }
        
        public string Position { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal PointsPerGame { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal ReboundsPerGame { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal AssistsPerGame { get; set; }

        public int TeamId { get; set; }
    }
}