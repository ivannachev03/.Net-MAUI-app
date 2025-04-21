using PMU_APP.Models;
using SQLite;

namespace PMU_APP.Services;

public class CarDatabaseService
{
    private SQLiteAsyncConnection _database;

    public CarDatabaseService()
    {
        Initialize();
    }

    private async Task Initialize()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "cars.db3"),
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        await _database.CreateTableAsync<Car>();
    }

    public async Task<List<Car>> GetCarsAsync()
    {
        await Initialize();
        return await _database.Table<Car>().ToListAsync();
    }

    public async Task<int> AddCarAsync(Car car)
    {
        await Initialize();
        return await _database.InsertAsync(car);
    }

    public async Task<int> DeleteCarAsync(Car car)
    {
        await Initialize();
        return await _database.DeleteAsync(car);
    }
}