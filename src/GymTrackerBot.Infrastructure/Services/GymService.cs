// using GymTrackerBot.Domain.Models;
// using GymTrackerBot.Domain.Interfaces;
// using GymTrackerBot.Domain.ResultPattern;
//
// namespace GymTrackerBot.Infrastructure.Services;
//
// public class GymService : IGymService
// {
//     private readonly IGymRepository _repository;
//
//     public GymService(IGymRepository repository)
//     {
//         _repository = repository;
//     }
//
//     public OperationResult<GymSession> StartSession(long userId, string? notes = null)
//     {
//         var session = _repository.GetSession(userId);
//
//         if (session != null && !session.IsFinished())
//         {
//             return OperationResult<GymSession>.Fail("A gym session is already active for this user.");
//         }
//
//         var newSession = new GymSession
//         {
//             UserId = userId,
//             StartTime = DateTime.UtcNow,
//             Notes = notes
//         };
//
//         _repository.AddSession(newSession);
//         return OperationResult<GymSession>.Ok(newSession);
//     }
//
//     public OperationResult<GymSession> EndSession(long userId)
//     {
//         var session = _repository.GetSession(userId);
//         if (session == null || session.IsFinished())
//         {
//             return OperationResult<GymSession>.Fail("No active gym session found for this user.");
//         }
//
//         session.EndTime = DateTime.UtcNow;
//
//         _repository.TryUpdateSession(session);
//
//         return OperationResult<GymSession>.Ok(session);
//     }
//
//     public OperationResult<bool> IsSessionActive(long userId)
//     {
//         var session = _repository.GetSession(userId);
//         if (session == null)
//         {
//             return OperationResult<bool>.Fail("No gym session found for this user.");
//         }
//
//         return OperationResult<bool>.Ok(!session.IsFinished());
//     }
// }