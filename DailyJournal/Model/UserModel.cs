using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyJournal.Model
{
    public class UserModel
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int UserID {  get; set; }

        [Required]
        [Unique]
        public string UserName { get; set; }

        public string? Image {  get; set; }


        [DataType(DataType.Password)]
        [StringLength(20)]
        [MinLength(8)]
        public string Password { get; set; }

        [Ignore]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string? ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastLoginAt { get; set; } = DateTime.Now;

        public string? PreferredTheme {  get; set; } 
    }
}
