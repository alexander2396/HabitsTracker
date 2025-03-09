using HabitsTracker.Persistence.Entities;
using SQLite;

namespace HabitsTracker.Persistence;

public class HabitDatabase
{
    string _databasePath;
    SQLiteAsyncConnection database;

    public HabitDatabase(string databasePath)
    {
        _databasePath = databasePath;
    }

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(_databasePath, Constants.Flags);
        var result = await database.CreateTableAsync<HabitEntity>();
    }

    public async Task<List<HabitEntity>> GetAllAsync()
    {
        await Init();
        return await database.Table<HabitEntity>().ToListAsync();
    }

    public async Task<int> CreateAsync(HabitEntity item)
    {
        await Init();
        return await database.InsertAsync(item);
    }

    public async Task ClearAll()
    {
        await database.DeleteAllAsync<HabitEntity>();
    }
}
