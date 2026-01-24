using DailyJournal.Database;
using DailyJournal.Model;
using SQLite;
using System;
using System.Threading.Tasks;

namespace DailyJournal.Service
{
    public class AuthService
    {
        private readonly SQLiteAsyncConnection _connection;

        public AuthService(AppDatabase database)
        {
            _connection = database.Connection;
        }

        // Register user
        public async Task<(bool Success, string Message)> RegisterAsync(UserModel user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
                    return (false, "Username and password are required");

                // Check existing username
                var existingUser = await _connection.Table<UserModel>()
                    .FirstOrDefaultAsync(u => u.UserName == user.UserName);

                if (existingUser != null)
                    return (false, "Username already exists");

                if (user.Password != user.ConfirmPassword)
                    return (false, "Passwords do not match");

                // Save in SQLite (consider hashing passwords before storing in production)
                await _connection.InsertAsync(user);

                Console.WriteLine($"User registered: {user.UserName}");
                return (true, "Registration successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegisterAsync error: {ex.Message}");
                return (false, "Registration failed");
            }
        }

        // Login user
        public async Task<(bool Success, UserModel? User, string Message)> LoginAsync(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return (false, null, "Enter both username and password");

                var user = await _connection.Table<UserModel>()
                    .Where(u => u.UserName == username && u.Password == password)
                    .FirstOrDefaultAsync();

                if (user == null)
                    return (false, null, "Invalid username or password");

                user.LastLoginAt = DateTime.Now;
                await _connection.UpdateAsync(user);

                return (true, user, "Login successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoginAsync error: {ex.Message}");
                return (false, null, "Login failed");
            }
        }
    }
}
