using PMU_APP.Models;
using PMU_APP.Utils;
using SQLite;

namespace PMU_APP.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;
        private static async Task Init()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<User>();
        }

        public static async Task<string> RegisterUser(User user)
        {
            await Init();

            if(!Validators.IsValidUsername(user.Username))
                return "Invalid username. Use 3-20 letters or numbers.";

            if (!Validators.IsValidEmail(user.Email))
                return "Invalid email address.";

            if (!Validators.IsValidPassword(user.Password))
                return "Password must be at least 8 characters, include upper & lower case letters, a number, and a special character.";

            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _database.InsertAsync(user);
                return "Success";
            }
            catch (SQLiteException)
            {
                return "Registration failed. Username may already exist.";
            }
        }

        public static async Task<User> Login(string identifier, string password)
        {
            await Init();

            var user = await _database.Table<User>()
                .Where(u => u.Username == identifier || u.Email == identifier)
                .FirstOrDefaultAsync();

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public static async Task<bool> UsernameExists(string username)
        {
            await Init();

            var user = await _database.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            return user != null;
        }

        public static async Task<bool> EmailExists(string email)
        {
            await Init();

            var user = await _database.Table<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user != null;
        }
    }
}
