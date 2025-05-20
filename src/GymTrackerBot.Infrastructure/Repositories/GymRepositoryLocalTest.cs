// using GymTrackerBot.Domain.Models;
// using GymTrackerBot.Domain.Interfaces;
//
// namespace GymTrackerBot.Infrastructure.Repositories;
//
// public class GymRepositoryLocalTest : IGymRepository
// {
//     private readonly Dictionary<long, GymSession> _sessions;
//
//     public GymRepositoryLocalTest()
//     {
//         _sessions = new Dictionary<long, GymSession>();
//     }
//
//     public bool TryUpdateSession(GymSession session)
//     {
//         if (_sessions.ContainsKey(session.UserId))
//         {
//             _sessions[session.UserId] = session;
//             return true;
//         }
//
//         return false;
//     }
//
//     public GymSession? GetSession(long userId)
//     {
//         _sessions.TryGetValue(userId, out var session);
//         return session;
//     }
//
//     public void AddSession(GymSession session)
//     {
//         _sessions[session.UserId] = session;
//     }
//
//     public void RemoveSession(long userId)
//     {
//         _sessions.Remove(userId);
//     }
// }