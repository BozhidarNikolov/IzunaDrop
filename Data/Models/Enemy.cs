using System.ComponentModel.DataAnnotations;

namespace IzunaDrop.Data.Models
{
    public class Enemy
    {
        [Required]
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }

        public int MyProperty { get; set; }
    }
}
