﻿using Microsoft.EntityFrameworkCore;
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
        [MaxLength(GameConstants.NameMaxLength)]
        [MinLength(GameConstants.NameMinLength)]
        public string Name { get; set; } = string.Empty; //gurantee will never be null

        [Required]
        [MaxLength(GameConstants.DescriptionMaxLength)]
        [MinLength(GameConstants.DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;
        
        public bool IsDeleted { get; set; } = false;

        [Required]
        public Genre Genre { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string Developer { get; set; } = string.Empty;
        [Required]
        public string Publisher { get; set; } = string.Empty;

        public ICollection<Enemy> Enemies { get; set; } = new List<Enemy>();

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<Character> Characters { get; set; } = new List<Character>();

    }
}
