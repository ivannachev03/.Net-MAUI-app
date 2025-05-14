using PMU_APP.Models;
using SQLite;
using System.Diagnostics;

namespace PMU_APP.Services;

public class CarDatabaseService
{
    private SQLiteAsyncConnection _database;

    public CarDatabaseService()
    {
        Initialize();
    }
    public async Task Initialize()
    {
        if (_database != null)
        {
            Debug.WriteLine("Database already initialized");
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "cars.db");
        Debug.WriteLine($"Database path: {databasePath}");

        _database = new SQLiteAsyncConnection(
            databasePath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        // Debug table creation
        var result = await _database.CreateTableAsync<Car>();
        Debug.WriteLine($"Table creation result: {result}");

        // Verify table exists
        var tableInfo = await _database.GetTableInfoAsync("Car");
        Debug.WriteLine($"Table columns: {tableInfo.Count}");
    }
   

    public async Task<List<Car>> GetCarsAsync()
    {
        await Initialize();
        return await _database.Table<Car>().ToListAsync();
    }
    public async Task<Car> GetCarAsync(int id)
    {
        await Initialize();
        return await _database.Table<Car>().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<int> AddCarAsync(Car car)
    {
        await Initialize();
        return await _database.InsertAsync(car);
    }

    public async Task<int> DeleteCarAsync(Car car)
    {
        await Initialize();

        
        Debug.WriteLine($"Deleting car ID: {car?.Id}, Brand: {car?.Brand}");

        if (car == null || car.Id <= 0)
        {
            throw new ArgumentException("Car must have a valid ID");
        }

        return await _database.DeleteAsync<Car>(car.Id);
    }
}