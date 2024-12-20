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
        [MinLength(EnemyConstants.NameMinLength)]
        [MaxLength(EnemyConstants.NameMaxLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(EnemyConstants.DescriptionMinLength)]
        [MaxLength(EnemyConstants.DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;
        
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(GameId))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
