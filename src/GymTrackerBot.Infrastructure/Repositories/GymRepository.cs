// using GymTrackerBot.Domain.Interfaces;
// using GymTrackerBot.Domain.Models;
// using Microsoft.EntityFrameworkCore;
//
// namespace GymTrackerBot.Infrastructure.Repositories;
//
// public class GymRepository : IGymRepository
// {
//     private readonly AppDbContext _db;
//
//     public GymRepository(AppDbContext db)
//     {
//         _db = db;
//     }
//
//     public void AddSession(GymSession session)
//     {
//         var existing = _db.GymSessions.Find(session.UserId);
//         if (existing != null)
//         {
//             existing.StartTime = session.StartTime;
//             existing.EndTime = session.EndTime;
//             existing.Notes = session.Notes;
//         }
//         else
//         {
//             _db.GymSessions.Add(session);
//         }
//
//         _db.SaveChanges();
//     }
//
//     public bool TryUpdateSession(GymSession session)
//     {
//         var existing = _db.GymSessions.Find(session.UserId);
//         if (existing == null)
//             return false;
//
//         existing.StartTime = session.StartTime;
//         existing.EndTime = session.EndTime;
//         existing.Notes = session.Notes;
//
//         _db.SaveChanges();
//         return true;
//     }
//
//     public GymSession? GetSession(long userId)
//     {
//         return _db.GymSessions.Find(userId);
//     }
// }
