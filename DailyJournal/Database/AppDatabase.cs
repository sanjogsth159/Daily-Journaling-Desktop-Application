using DailyJournal.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace DailyJournal.Database
{
    public class AppDatabase
    {
        private static AppDatabase _instance;
        public static AppDatabase Instance => _instance ??= new AppDatabase();

        // Expose path so other code can read it if needed
        public string DatabasePath { get; }

        public SQLiteAsyncConnection Connection { get; }

        private AppDatabase()
        {
            DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "DailyJournal.db3");
            Connection = new SQLiteAsyncConnection(DatabasePath);
        }

        public async Task InitializeAsync()
        {
            // Ensure tables for models used by the app exist
            await Connection.CreateTableAsync<UserModel>();
            await Connection.CreateTableAsync<EntryModel>();
        }

        // User helpers
        public Task<int> AddUserAsync(UserModel user) => Connection.InsertAsync(user);

        public Task<UserModel?> GetUserByUsernameAsync(string username)
            => Connection.Table<UserModel>().Where(u => u.UserName == username).FirstOrDefaultAsync();

        public Task<int> UpdateUserAsync(UserModel user) => Connection.UpdateAsync(user);

        // Entry helpers
        public Task<int> AddEntryAsync(EntryModel entry) => Connection.InsertAsync(entry);

        public Task<int> UpdateEntryAsync(EntryModel entry)
        {
            entry.UpdatedAt = DateTime.UtcNow;
            return Connection.UpdateAsync(entry);
        }

        public Task<int> DeleteEntryAsync(int entryId) => Connection.DeleteAsync<EntryModel>(entryId);

        public Task<EntryModel?> GetEntryByDateAsync(DateTime date)
            => Connection.Table<EntryModel>()
                         .Where(e => e.Date == date.Date)
                         .FirstOrDefaultAsync();

        public Task<List<EntryModel>> GetAllEntriesAsync()
            => Connection.Table<EntryModel>().OrderByDescending(e => e.Date).ToListAsync();

        // Convenience: delete all entries (use with caution)
        public Task<int> DeleteAllEntriesAsync() => Connection.ExecuteAsync("DELETE FROM \"EntryModel\"");
    }
}
