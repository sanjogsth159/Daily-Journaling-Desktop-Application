using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace DailyJournal.Model
{
    public class UserModel
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Required]
        [Unique]
        public string UserName { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [StringLength(20)]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Ignore]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastLoginAt { get; set; } = DateTime.Now;

        public string? PreferredTheme { get; set; }
    }
}
