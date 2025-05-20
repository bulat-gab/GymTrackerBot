using GymTrackerBot.Domain.Versioning;

namespace GymTrackerBot.Domain.Models;

// public class GymSession
// {
//     public int Id { get; set; }
//     public long UserId { get; set; }
//     public string? Username { get; set; }
//     public DateTime StartTime { get; set; }
//     public DateTime? EndTime { get; set; }
//     public string? Notes { get; set; }
//
//     public bool IsFinished() => EndTime.HasValue;
// }

public class GymSession : IVersionedModel
{
    public static readonly int CurrentVersion = 0;

    public int Id { get; set; }
    public string? Username { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Notes { get; set; }
    // public List<Exercise> Exercises { get; set; } = new();
    
    public bool IsFinished() => EndTime.HasValue;
        
    public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : null;
        
    public DateTime GetDateForCalendar() => StartTime.Date;

    public int SchemaVersion { get; } = CurrentVersion;

    public IVersionedModel UpgradeToLatest()
    {
        throw new NotImplementedException();
    }
}
