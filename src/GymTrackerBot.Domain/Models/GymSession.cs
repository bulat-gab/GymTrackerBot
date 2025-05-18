namespace GymTrackerBot.Domain.Models;

public class GymSession
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public string? Username { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Notes { get; set; }

    public bool IsFinished() => EndTime.HasValue;
}