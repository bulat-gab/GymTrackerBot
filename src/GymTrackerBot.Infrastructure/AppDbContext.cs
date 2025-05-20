// using Microsoft.EntityFrameworkCore;
// using GymTrackerBot.Domain.Models;
//
// namespace GymTrackerBot.Infrastructure;
//
// public class AppDbContext : DbContext
// {
//     public DbSet<GymSession> GymSessions => Set<GymSession>();
//
//     public AppDbContext(DbContextOptions<AppDbContext> options)
//         : base(options)
//     {
//     }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<GymSession>()
//             .HasKey(x => x.UserId);
//     }
// }