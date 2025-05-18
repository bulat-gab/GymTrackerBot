using GymTrackerBot.Domain.Models;

namespace GymTrackerBot.Domain.Interfaces;

public interface IGymRepository
{
    void AddSession(GymSession session);
    bool TryUpdateSession(GymSession session);
    GymSession? GetSession(long userId);
}