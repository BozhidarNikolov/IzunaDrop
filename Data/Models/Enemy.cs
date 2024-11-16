using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IzunaDrop.Constants;

namespace IzunaDrop.Data.Models
{
    public class Enemy
    {
        [Required]
        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(EnemyConstants.EnemyNameMinLength)]
        [MaxLength(EnemyConstants.EnemyNameMaxLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(EnemyConstants.EnemyDescriptionMinLength)]
        [MaxLength(EnemyConstants.EnemyDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        
        public string? ImagePath { get; set; }

        [ForeignKey(nameof(GameId))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
