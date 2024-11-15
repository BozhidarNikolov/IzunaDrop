using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IzunaDrop.Data.Models
{
    public class Game
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; } = string.Empty; //gurantee will never be null

    }
}
