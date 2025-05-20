using GymTrackerBot.Domain;
using GymTrackerBot.Domain.Versioning;
using Microsoft.JSInterop;
using System.Text.Json;
using GymTrackerBot.Domain.Interfaces;
using GymTrackerBot.Domain.Models;

namespace GymTrackerBot.Infrastructure.Services
{
    public class LocalStorageGymSessionService : IGymServiceStandalone
    {
        private readonly IJSRuntime _jsRuntime;
        private const string STORAGE_KEY = "gym-tracker-sessions";

        public LocalStorageGymSessionService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task<List<GymSession>> GetAllSessionsFromStorageAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", STORAGE_KEY);

            if (string.IsNullOrEmpty(json))
                return new List<GymSession>();

            var sessions = JsonSerializer.Deserialize<List<GymSession>>(json) ?? new List<GymSession>();

            // Check and upgrade versions if needed
            for (int i = 0; i < sessions.Count; i++)
            {
                if (_versionService.NeedsUpgrade(sessions[i]))
                {
                    sessions[i] = (GymSession)_versionService.Upgrade(sessions[i]);
                }
            }

            return sessions;
        }

        private async Task SaveAllSessionsToStorageAsync(List<GymSession> sessions)
        {
            var json = JsonSerializer.Serialize(sessions);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }

        public async Task<List<GymSession>> GetSessionsAsync(long userId)
        {
            var sessions = await GetAllSessionsFromStorageAsync();
            return sessions.Where(s => s.UserId == userId).ToList();
        }

        public async Task<GymSession?> GetSessionAsync(int id)
        {
            var sessions = await GetAllSessionsFromStorageAsync();
            return sessions.FirstOrDefault(s => s.Id == id);
        }

        public async Task<List<DateTime>> GetSessionDatesInMonthAsync(long userId, DateTime month)
        {
            var sessions = await GetSessionsAsync(userId);

            return sessions
                .Where(s => s.StartTime.Year == month.Year && s.StartTime.Month == month.Month)
                .Select(s => s.StartTime.Date)
                .Distinct()
                .ToList();
        }

        public async Task<List<GymSession>> GetSessionsForDateAsync(long userId, DateTime date)
        {
            var sessions = await GetSessionsAsync(userId);

            return sessions
                .Where(s => s.StartTime.Date == date.Date)
                .ToList();
        }

        public async Task<GymSession> CreateSessionAsync(GymSession session)
        {
            var sessions = await GetAllSessionsFromStorageAsync();

            // Generate a unique ID
            session.Id = sessions.Count > 0 ? sessions.Max(s => s.Id) + 1 : 1;

            sessions.Add(session);
            await SaveAllSessionsToStorageAsync(sessions);

            return session;
        }

        public async Task<GymSession> UpdateSessionAsync(GymSession session)
        {
            var sessions = await GetAllSessionsFromStorageAsync();

            var index = sessions.FindIndex(s => s.Id == session.Id);
            if (index >= 0)
            {
                sessions[index] = session;
                await SaveAllSessionsToStorageAsync(sessions);
            }

            return session;
        }

        public async Task<bool> DeleteSessionAsync(int id, long userId)
        {
            var sessions = await GetAllSessionsFromStorageAsync();

            var session = sessions.FirstOrDefault(s => s.Id == id && s.UserId == userId);
            if (session != null)
            {
                sessions.Remove(session);
                await SaveAllSessionsToStorageAsync(sessions);
                return true;
            }

            return false;
        }

        public async Task<GymSession> FinishSessionAsync(int id, long userId, DateTime endTime, string? notes = null)
        {
            var session = await GetSessionAsync(id);

            if (session != null && session.UserId == userId)
            {
                session.EndTime = endTime;

                if (!string.IsNullOrEmpty(notes))
                {
                    session.Notes = string.IsNullOrEmpty(session.Notes)
                        ? notes
                        : $"{session.Notes}\n\n{notes}";
                }

                return await UpdateSessionAsync(session);
            }

            throw new InvalidOperationException("Session not found or not owned by user");
        }
    }
}