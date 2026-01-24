csharp DailyJournal\Model\UserModel.cs
using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace DailyJournal.Model
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Required]
        [Unique]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;  // Only one password

        [Ignore]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
    }
}