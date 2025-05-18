namespace GymTrackerBot.Domain.Interfaces;

using GymTrackerBot.Domain.Models;
using GymTrackerBot.Domain.ResultPattern;

public interface IGymService
{
    OperationResult<GymSession> StartSession(long userId, string? notes);
    OperationResult<GymSession> EndSession(long userId);
    OperationResult<bool> IsSessionActive(long userId);
}