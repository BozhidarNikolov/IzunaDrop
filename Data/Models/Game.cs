using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using IzunaDrop.Constants;
using IzunaDrop.Constants.Enums;

namespace IzunaDrop.Data.Models
{
    public class Game
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameConstants.GameNameMaxLength)]
        [MinLength(GameConstants.GameNameMinLength)]
        public string Name { get; set; } = string.Empty; //gurantee will never be null

        [Required]
        [MaxLength(GameConstants.GameDescriptionMaxLength)]
        [MinLength(GameConstants.GameDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public Genre Genre { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string Developer { get; set; } = string.Empty;
        [Required]
        public string Publisher { get; set; } = string.Empty;
        public string? ImagePath { get; set; } 





    }
}
