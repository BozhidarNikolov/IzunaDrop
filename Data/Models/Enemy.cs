using System.ComponentModel.DataAnnotations;
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
    }
}
