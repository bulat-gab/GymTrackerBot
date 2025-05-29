namespace GymTrackerBot.Domain.Models;

public class GymExerciseTemplate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public GymSessionType SessionType { get; set; } // stored as int in JSON
    public int DefaultSets { get; set; }
    public int DefaultReps { get; set; }
}
