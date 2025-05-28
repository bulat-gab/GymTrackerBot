namespace GymTrackerBot.Domain.Models;

[Flags]
public enum GymSessionType
{
    None = 0,
    Legs = 1 << 0,
    Chest = 1 << 1,
    Back = 1 << 2,
    Shoulders = 1 << 3,
    Core = 1 << 4,
    CrossFit = 1 << 5,
    Cardio = 1 << 6,
    FullBody = 1 << 7,
    Mobility = 1 << 8,
    Arms = 1 << 9,
}
