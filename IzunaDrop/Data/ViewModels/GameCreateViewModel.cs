using IzunaDrop.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IzunaDrop.ViewModels
{
    public class GameCreateViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)] // Adjust to your constants if needed
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(250, MinimumLength = 10)] // Adjust to your constants if needed
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
        public IFormFile? ImageFile { get; set; }

    }
}
