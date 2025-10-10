using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomerAuth.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(username), IsUnique = true)]
    public class Useraccount
    {
        [Key]
        public int id { get; set; }

        [Required, MaxLength(50)]
        public string First_name { get; set; }

        [Required, MaxLength(50)]
        public string Last_name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        public string? ProfileImagePath { get; set; }

        public bool IsBlocked { get; set; } = false;
    }
}
