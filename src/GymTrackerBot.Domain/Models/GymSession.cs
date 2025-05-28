using GymTrackerBot.Domain.Versioning;

namespace GymTrackerBot.Domain.Models;

public class GymSession : IVersionedModel
{
    public static readonly int CurrentVersion = 0;

    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Notes { get; set; }
    public GymSessionType SessionType { get; set; } = GymSessionType.None;

    public List<GymExercise> Exercises { get; set; } = new();
    
    public bool IsFinished() => EndTime.HasValue;
        
    public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : null;
        
    public DateTime GetDateForCalendar() => StartTime.Date;

    public int SchemaVersion => CurrentVersion;

    public IVersionedModel UpgradeToLatest() => throw new NotImplementedException();
}
