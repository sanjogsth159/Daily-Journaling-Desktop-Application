using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace DailyJournal.Model
{
    public class EntryModel
    {
        [PrimaryKey, AutoIncrement]
        public int EntryID { get; set; }

        // Optional title (searchable)
        public string? Title { get; set; }

        // Date only (store as UTC date)
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? Mood { get; set; }

        // Primary mood used for analytics (new)
        public string PrimaryMood { get; set; } = string.Empty;

        // Optional secondary moods (up to two)
        public string? SecondaryMood1 { get; set; }
        public string? SecondaryMood2 { get; set; }

        public string? Tags { get; set; }
        [Ignore]
        public int WordCount => string.IsNullOrWhiteSpace(Content)
            ? 0
            : Content.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
