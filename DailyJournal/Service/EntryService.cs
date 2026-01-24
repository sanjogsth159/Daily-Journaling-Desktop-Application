using DailyJournal.Database;
using DailyJournal.Model;
    using System;
using System.Threading.Tasks;

namespace DailyJournal.Service
{
    public class EntryService
    {
        public async Task<EntryModel> GetTodayEntryAsync()
        {
            var today = DateTime.UtcNow.Date;
            var existing = await AppDatabase.Instance.GetEntryByDateAsync(today);
            if (existing != null) return existing;

            // return new empty one for today
            return new EntryModel { Date = today };
        }

        public async Task<(bool Success, string Message)> SaveAsync(EntryModel entry)
        {
            try
            {
                if (entry.EntryID == 0)
                {
                    entry.CreatedAt = DateTime.UtcNow;
                    entry.UpdatedAt = DateTime.UtcNow;
                    await AppDatabase.Instance.AddEntryAsync(entry);
                    return (true, "Saved");
                }
                else
                {
                    entry.UpdatedAt = DateTime.UtcNow;
                    await AppDatabase.Instance.UpdateEntryAsync(entry);
                    return (true, "Updated");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int entryId)
        {
            try
            {
                await AppDatabase.Instance.DeleteEntryAsync(entryId);
                return (true, "Deleted");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
