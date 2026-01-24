using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace DailyJournal.Model
{
    public class EntryModel
    {
        [PrimaryKey, AutoIncrement]
        public int EntryID { get; set; }

        // Date only (store as UTC date)
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? Mood { get; set; }

        // Not persisted; convenience property
        [Ignore]
        public int WordCount => string.IsNullOrWhiteSpace(Content)
            ? 0
            : Content.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
