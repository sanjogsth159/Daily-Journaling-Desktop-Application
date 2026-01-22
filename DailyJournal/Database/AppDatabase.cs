using DailyJournal.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyJournal.Database
{
    public class AppDatabase
    {
        public SQLiteAsyncConnection Connection { get; private set; }

        public AppDatabase()
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "DailyJournal.db3");
            Connection = new SQLiteAsyncConnection(databasePath);

            try
            {
                // Synchronously create table
                Connection.CreateTableAsync<UserModel>().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization error: {ex.Message}");
                throw; // Will show error if DB fails
            }
        }
    }
}
