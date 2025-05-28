namespace GymTrackerBot.Domain.Models;

public class GymExercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public string? Notes { get; set; }
}