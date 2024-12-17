using IzunaDrop.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IzunaDrop.Data.Models
{
    public class Item
    {
        [Required]
        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(ItemConstants.NameMinLength)]
        [MaxLength(ItemConstants.NameMaxLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(ItemConstants.DescriptionMinLength)]
        [MaxLength(ItemConstants.DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsDeleted { get; set; } = false;
        public string? ImagePath { get; set; }

        [ForeignKey(nameof(GameId))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
