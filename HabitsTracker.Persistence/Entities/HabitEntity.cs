using SQLite;

namespace HabitsTracker.Persistence.Entities;

public class HabitEntity
{
    public int Id { get; set; }

    public string Name { get; set; }
}
