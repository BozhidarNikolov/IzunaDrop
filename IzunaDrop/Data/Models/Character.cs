using IzunaDrop.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IzunaDrop.Data.Models
{
    public class Character
    {
        [Required]
        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(CharacterConstants.NameMinLength)]
        [MaxLength(CharacterConstants.NameMaxLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(CharacterConstants.DescriptionMinLength)]
        [MaxLength(CharacterConstants.DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;



       
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(GameId))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
